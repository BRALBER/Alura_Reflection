using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Aula_Reflection.Controller;

namespace Aula_Reflection.Infraestrutura
{
    internal class WebApplication
    {
        private readonly string[] _prefixos;
        public WebApplication(string[] prefixos)
        {
            if (prefixos == null)
            {
                throw new ArgumentNullException(nameof(prefixos));
            }
            _prefixos = prefixos;
        }

        public void Iniciar()
        {
            while (true)
            {
                ManipularRequisitos();
            }
        }

        public void ManipularRequisitos()
        {
            var httpListener = new HttpListener();
            foreach (var p in _prefixos)
            {
                httpListener.Prefixes.Add(p);
            }
            httpListener.Start();

            var contexto = httpListener.GetContext();
            var requisicao = contexto.Request;
            var resposta = contexto.Response;

            if (requisicao.Url == null)
            {
                resposta.StatusCode = 404;
                httpListener.Close();
                return;
            }
            var path = requisicao.Url.PathAndQuery;
            if (Utilidades.EhArquivo(path))
            {
                var manipulador = new ManipuladorRequisicaoArquivo();
                manipulador.Manipular(resposta, path);
            }
            else 
            {
                var manipulador = new ManipuladorRequisicaoController();
                manipulador.Manipular(resposta, path);
            }
            httpListener.Stop();
        }
    }
}
