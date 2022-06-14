using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aula_Reflection.Services.Cambio
{
    public class CambioTesteService : ICambioServices
    {
        private readonly Random _rd = new Random();
        public decimal Calcular(string moedaOrigem, string moedaDestino, decimal valor) => valor * (decimal)_rd.NextDouble();
    }
}