using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aula_Reflection.Infraestrutura
{
    public static class Utilidades
    {
        public static bool EhArquivo(string path)
        {
            var ultimaParte = path.Split('/').Last();
            return ultimaParte.Contains(".");
        }
        public static string ConverterPathParaNomeAssembly(string path)
        {
            var prefixoAssembly = "Aula_Reflection";
            var pathComPontos = path.Replace('/', '.');

            return $"{prefixoAssembly}{pathComPontos}";
        }
        public static string ObterTipoDeConteudo(string path)
        {
            if (path.EndsWith(".css"))
            {
                return "text/css; charset=utf-8";
            }
            else if (path.EndsWith(".js"))
            {
                return "application/js; charset=utf-8";
            }
            else if (path.EndsWith(".html"))
            {
                return "text/html; charset=utf-8";
            }
            throw new ArgumentException();
            
        }
    }
}