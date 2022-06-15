using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Aula_Reflection.Infraestrutura
{
    internal class ManipuladorRequisicaoController
    {
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

            var methodInfo = controller.GetType().GetMethod(actionName);
            var resultadoAction = (string) methodInfo.Invoke(controller, new object[0]);

            var buffer = Encoding.UTF8.GetBytes(resultadoAction);
            respota.StatusCode = 200;
            respota.ContentType = "text/html; charset=utf-8";
            respota.ContentLength64 = buffer.Length;

            respota.OutputStream.Write(buffer, 0, buffer.Length);
            respota.OutputStream.Close();

        }
    }
}
