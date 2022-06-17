using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Aula_Reflection.Infraestrutura.Binding
{
    public class ActionBindInfo
    {
        public MethodInfo MethodInfo { get; private set; }
        public IReadOnlyCollection<ArgumentoNomeValor> ArgumentoNomeValor { get; private set; }

        public ActionBindInfo(MethodInfo methodinfo, IEnumerable<ArgumentoNomeValor> argumentoNomeValor)
        {
            MethodInfo = methodinfo ?? throw new ArgumentNullException(nameof(methodinfo));
            ArgumentoNomeValor =
                new ReadOnlyCollection<ArgumentoNomeValor>(argumentoNomeValor.ToList())
                ?? throw new ArgumentNullException(nameof(argumentoNomeValor));
        }

        public object? Invoke(object controller)
        {
            var countParamtros = ArgumentoNomeValor.Count;
            
            if (countParamtros == 0)
                return MethodInfo.Invoke(controller, new object[0]);

            var parametrosMethodInfo = MethodInfo.GetParameters();
            var parametrosInvoke = new Object[countParamtros];
            for (int i = 0; i < countParamtros; i++)
            {
                var parametro = parametrosMethodInfo[i];
                
                var argumento = ArgumentoNomeValor.Single(tupla => tupla.Nome == parametro.Name);
            
                parametrosInvoke[i] = Convert.ChangeType(argumento.Valor, parametro.ParameterType);
            }
            return MethodInfo.Invoke(controller, parametrosInvoke);
                   
        }
    }
}