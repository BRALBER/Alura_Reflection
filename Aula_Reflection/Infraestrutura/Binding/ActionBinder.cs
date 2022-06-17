using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Aula_Reflection.Infraestrutura.Binding
{
    public class ActionBinder
    {
        /*
        /Cambio/Calculo?moedaOrigem=BRL&moedaDestino=USD&valor=10
        /Cambio/Calculo?moedaDestino=USD&moedaOrigem=BRL&valor=10
        /Cambio/Calculo?valor=10&moedaDestino=USD&moedaOrigem=BRL

        /Cambio/Calculo?
        moedaDestino=USD&valor=10
        */

        public ActionBindInfo ExtrairActionBindInfo(object controller, string url)
        {
            var indexInterrogacao = url.IndexOf('?');
            if (indexInterrogacao < 0)
            {
                var nomeAction = url.Split('/', StringSplitOptions.RemoveEmptyEntries).Last();
                var methodInfo = controller.GetType().GetMethod(nomeAction) ?? throw new ArgumentException("url Invalida", nameof(url));
                return new ActionBindInfo(methodInfo, Enumerable.Empty<ArgumentoNomeValor>());
            }
            else
            {
                var nomeAction =  url.Substring(0, indexInterrogacao).Split('/', StringSplitOptions.RemoveEmptyEntries).Last();

                var parametrosUrl = url.Substring(indexInterrogacao + 1);
                var tuplasNomeValor = ObterArgumentosNomeValores(parametrosUrl);

                var argumentos = tuplasNomeValor.Select(tupla => tupla.Nome).ToArray();
                var methodInfo = MetodoParaMethodInfo(nomeAction, argumentos, controller);

                return new ActionBindInfo(methodInfo, tuplasNomeValor);
            }
        }

        private IEnumerable<ArgumentoNomeValor> ObterArgumentosNomeValores(string querryString)
        {
            var tuplasNomeValor = querryString.Split('&', StringSplitOptions.RemoveEmptyEntries);
            foreach(string t in tuplasNomeValor)
            {
                var fragmentos = t.Split('=');
                yield return new ArgumentoNomeValor(fragmentos[0], fragmentos[1]);
            }
        }
        private MethodInfo MetodoParaMethodInfo(string nomeAction, string[] parametros, object objeto)
        {
            var flags = BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance;
            var metodos = objeto.GetType().GetMethods(flags);
            
            var erroNomeAction = true;
            foreach (var m in metodos)
            {
                if(m.Name != nomeAction)
                    continue;
                
                erroNomeAction = false;
                var argumentosMetodo = m.GetParameters();
                
                if (argumentosMetodo.Length != parametros.Length)
                    continue;
                
                //Retorna true se todos os elementos da condição forem verdadeiros
                var temMesmosArgumentos = argumentosMetodo.All(p => parametros.Contains(p.Name));
                if (temMesmosArgumentos)
                {
                    return m;
                }
            }
            if (erroNomeAction == true)
                throw new ArgumentException($"Não foi possível encontrar o metodo {nomeAction} ", nameof(nomeAction));
            
            throw new ArgumentException ($"Não foi encontrada uma sobrecarga de {nomeAction} "
            + "com parametros inseridos", nameof(parametros));
        }
    }
}