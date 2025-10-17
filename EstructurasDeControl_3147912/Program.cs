using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructurasDeControl_3147912
{
    internal class Program
    {
        static void Main(string[] args) // El punto de partida
        {
            //// Tipo de DA
            //int num1 = 10;
            //string nombre = "samuel";
            //char letra = 'A';
            //decimal precio = 3.14m;
            //float altura = 1.75f;
            //bool esVerdadero = true;
            //DateTime fecha = DateTime.Now;

            //// estructura de Control
            //Console.WriteLine("ingrese su edad :   .");
            //int numero = int.Parse(Console.ReadLine());
            //Console.WriteLine($"su edad es: {numero} ");



            //Console.WriteLine("Ingrese su primera nota:");
            //decimal nota1 = decimal.Parse(Console.ReadLine());

            //if (nota1 < 1 || nota1 > 5)
            //{
            //    Console.WriteLine("Nota incorrecta, ingrese la correcta:");
            //    nota1 = decimal.Parse(Console.ReadLine());
            //}

            //Console.WriteLine("Ingrese su segunda nota:");
            //decimal nota2 = decimal.Parse(Console.ReadLine());

            //if (nota2 < 1 || nota2 > 5)
            //{
            //    Console.WriteLine("Nota incorrecta, ingrese la correcta:");
            //    nota2 = decimal.Parse(Console.ReadLine());
            //}

            //Console.WriteLine("Ingrese su tercera nota:");
            //decimal nota3 = decimal.Parse(Console.ReadLine());

            //if (nota3 < 1 || nota3 > 5)
            //{
            //    Console.WriteLine("Nota incorrecta, ingrese la correcta:");
            //    nota3 = decimal.Parse(Console.ReadLine());
            //}

            //decimal resultado = (nota1 * 0.2m) + (nota2 * 0.3m) + (nota3 * 0.5m);

            //Console.WriteLine("Su nota final es: " + resultado);

            //if (resultado >= 3)
            //{
            //    Console.WriteLine("¡Aprobó!");
            //}
            //else
            //{
            //    Console.WriteLine("No aprobó.");
            //}



            Console.WriteLine("Dame el PRecio de tu producto:   ");
            decimal precio = decimal.Parse(Console.ReadLine());

            if (precio >= 100000)
            {
                precio = precio - precio * 0.20m;
                Console.WriteLine($"Se aplica descuento, TOTAL: {precio}");
            }
            else
            {
                Console.WriteLine($"No se aplica descuento, TOTAL: {precio}");
            }
        }
    }
}
