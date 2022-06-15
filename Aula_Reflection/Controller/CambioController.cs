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
    public class CambioController:ControllerBase
    {
        private ICambioServices _cambioService;
        public CambioController()
        {
            _cambioService = new CambioTesteService();
        }
        public string MXN()
        {
            var valorFinal = _cambioService.Calcular("MXN", "BR", 1);
            var textoPagina = View();
            return textoPagina.Replace("VALOR_EM_REAIS", valorFinal.ToString());
        }

       public string USD()
        {
            var valorFinal = _cambioService.Calcular("USD", "BRL", 1);
            var textoPagina = View();
            return textoPagina.Replace("VALOR_EM_REAIS", valorFinal.ToString());
        }
    }
}