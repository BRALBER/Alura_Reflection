using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aula_Reflection.Services
{
    public interface ICambioServices
    {
        
        decimal Calcular(string moedaOrigem, string moedaDestino, decimal valor);
    }
}