using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Aula_Reflection.Controller
{
    public abstract class ControllerBase
    {
        protected string View([CallerMemberName]string nomeArquivo = null)
        {
            var nomeClasse = GetType().Name;
            var nomePagina = nomeClasse.Replace("Controller",""); 

            var assembly = Assembly.GetExecutingAssembly();

            var resourceName = $"Aula_Reflection.View.{nomePagina}.{nomeArquivo}.html";
            var resourceStream = assembly.GetManifestResourceStream(resourceName);
            using (var streamReader = new StreamReader(resourceStream))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
