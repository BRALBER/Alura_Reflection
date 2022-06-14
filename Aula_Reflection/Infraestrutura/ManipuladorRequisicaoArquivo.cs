using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Reflection;

namespace Aula_Reflection.Infraestrutura
{
    public class ManipuladorRequisicaoArquivo
    {
        public void Manipular(HttpListenerResponse resposta, string path)
        {

            var assembly = Assembly.GetExecutingAssembly();

            var nameResource = Utilidades.ConverterPathParaNomeAssembly(path);
            var resourceStream = assembly.GetManifestResourceStream(nameResource);
           
            if (resourceStream == null)
                resposta.StatusCode = 404;
            else
                using (resourceStream)
                {
                    var bytesResource = new byte[resourceStream.Length];
                    resourceStream.Read(bytesResource, 0, bytesResource.Length);

                    resposta.ContentType = Utilidades.ObterTipoDeConteudo(path);
                    resposta.StatusCode = 200;
                    resposta.ContentLength64 = bytesResource.Length;

                    resposta.OutputStream.Write(bytesResource, 0, bytesResource.Length);
                    resposta.OutputStream.Close();
                }
        }
    }
}