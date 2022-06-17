using System.Reflection;
using Aula_Reflection.Infraestrutura;

var prefixos = new string[] { "http://localhost:5341/" };
var web = new WebApplication(prefixos);
System.Console.WriteLine("Qualquer Coisa");
web.Iniciar();