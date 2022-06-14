using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Aula_Reflection.Services;
using Aula_Reflection.Services.Cambio;

namespace Aula_Reflection.Controller
{
    public class CambioController
    {
        private ICambioServices _cambioService;
        public CambioController()
        {
            _cambioService = new CambioTesteService();
        }
        public string MXN()
        {
            var assembly = Assembly.GetExecutingAssembly();
            
            var resourceName = "Aula_Reflection.View.Cambio.MXN.html";
            var resourceStream = assembly.GetManifestResourceStream(resourceName);
            using (var streamReader = new StreamReader(resourceStream))
            {
                var cambio = _cambioService.Calcular("MXN", "BR", 1);
                var paginaTexto = streamReader.ReadToEnd();
                return paginaTexto.Replace("VALOR_EM_REAIS", cambio.ToString());
            }
        }
       public string USD()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var resourceName = "Aula_Reflection.View.Cambio.USD.html";
            var resourceStream = assembly.GetManifestResourceStream(resourceName);
            using (var streamReader = new StreamReader(resourceStream))
            {
                var valorFinal = _cambioService.Calcular("USD", "BRL", 1);
                var textoPagina = streamReader.ReadToEnd();
                return textoPagina.Replace("VALOR_EM_REAIS", valorFinal.ToString());
            }
        }
    }
}