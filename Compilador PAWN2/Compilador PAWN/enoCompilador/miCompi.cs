using System;
using System.IO;

namespace Compilador
{
    class Micompi
    {
        static void Main(string[] args)
        {
            Lexico lexico = new Lexico();

            if (!lexico.ErrorEncontrado)
            {
                Console.WriteLine("Análisis léxico terminado");
                Sintaxis sintaxis = new Sintaxis(lexico.listaNodos);
                sintaxis.programa();
            }
            else
            {
                Console.WriteLine("Análisis terminado con errores");
            }
        }
    }
}