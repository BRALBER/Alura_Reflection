using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Aula_Reflection.Infraestrutura.Binding;

namespace Aula_Reflection.Infraestrutura
{
    internal class ManipuladorRequisicaoController
    {
        private readonly ActionBinder _actionBinder = new ActionBinder();
        public void Manipular(HttpListenerResponse respota, string path)
        {
            //Cambio/MXN        Cambio/USD
            //Cartao/Credito    Cartao/Debito
            var assemblyName = "Aula_Reflection";
            var partes = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
            var controllerName = partes[0];
            var actionName = partes[1];

            var nomeCompletoRecurso = $"{assemblyName}.Controller.{controllerName}Controller";
            var contollerWrapper = Activator.CreateInstance(assemblyName, nomeCompletoRecurso, new object[0]);

            //if (contollerWrapper == null)
              //  return;
            var controller = contollerWrapper.Unwrap();

            var ActionInfo = _actionBinder.ExtrairActionBindInfo(controller, path);
            var resultadoAction = (string) ActionInfo.Invoke(controller);

            var buffer = Encoding.UTF8.GetBytes(resultadoAction);
            respota.StatusCode = 200;
            respota.ContentType = "text/html; charset=utf-8";
            respota.ContentLength64 = buffer.Length;

            respota.OutputStream.Write(buffer, 0, buffer.Length);
            respota.OutputStream.Close();

        }
    }
}
