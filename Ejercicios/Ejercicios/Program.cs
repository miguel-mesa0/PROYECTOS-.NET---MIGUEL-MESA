using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicios
{
    internal class Program
    {
        //Ejercicios numero 1
        static void Main(string[] args)
        {
            Console.Write("Ingrese el monto del préstamo: ");
            double prestamo = double.Parse(Console.ReadLine());

            double tasa = 0.05;
            int años = 5;

            double interesAnual = prestamo * tasa;
            double interesTrimestre = interesAnual / 4;
            double interesMensual = interesAnual / 12;
            double totalInteres = interesAnual * años;
            double totalPagar = prestamo + totalInteres;

            Console.WriteLine("RESULTADOS");
            Console.WriteLine("Intereses en un año: " + interesAnual);
            Console.WriteLine("Intereses en el tercer trimestre: " + interesTrimestre);
            Console.WriteLine("Intereses en el primer mes: " + interesMensual);
            Console.WriteLine("Total de intereses en 5 años: " + totalInteres);
            Console.WriteLine("Total a pagar (préstamo + intereses): " + totalPagar);


            //ejercicio numero 2
            Console.Write("Ingrese el salario del empleado: ");
            double salario = double.Parse(Console.ReadLine());

            Console.Write("Ingrese el valor de ahorro mensual programado: ");
            double ahorro = double.Parse(Console.ReadLine());

            double salud = salario * 0.125;
            double pension = salario * 0.16;
            double totalRecibir = salario - (salud + pension + ahorro);

            Console.WriteLine("COLILLA DE PAGO ");
            Console.WriteLine($"Salario del empleado: {salario}");
            Console.WriteLine($"Ahorro mensual programado: {ahorro}");
            Console.WriteLine($"Descuento por salud 12.5%: {salud}");
            Console.WriteLine($"Descuento por pensión 16%: {pension}");
            Console.WriteLine($"TOTAL : {totalRecibir}");

            ////ejercicio numero 3
            Console.Write("Ingrese el valor total de la matrícula: ");
            double matricula = double.Parse(Console.ReadLine());

            double cuota1 = matricula * 0.40;
            double cuota2 = matricula * 0.25;
            double cuota3 = matricula * 0.20;
            double cuota4 = matricula * 0.15;

            Console.WriteLine("PLAN DE PAGOS");
            Console.WriteLine($"Primera cuota (40%): {cuota1}");
            Console.WriteLine($"Segunda cuota (25%): {cuota2}");
            Console.WriteLine($"Tercera cuota (20%): {cuota3}");
            Console.WriteLine($"Cuarta cuota (15%): {cuota4}");

            // Ejercicio 4
            Console.Write("Ingrese su nombre: ");

            string nombre = Console.ReadLine();

            Console.Write("Ingrese su dirección: ");

            string direccion = Console.ReadLine();

            Console.Write("Ingrese su año de nacimiento: ");

            int añoNacimiento = int.Parse(Console.ReadLine());
            int añoActual = 2025;
            int edad = añoActual - añoNacimiento;

            Console.WriteLine($"Nombre: {nombre}");
            Console.WriteLine($"Dirección: {direccion}");
            Console.WriteLine($"Año de nacimiento: {añoNacimiento}");
            Console.WriteLine($"Edad: {edad} años");

            // Ejercicio 5
            double tiempo1L = 1.5;
            double tiempo3L = 3 * tiempo1L;
            double tiempo5L = 5 * tiempo1L;

            Console.WriteLine($"Tiempo para llenar el balde de 1L: {tiempo1L} horas");
            Console.WriteLine($"Tiempo para llenar el balde de 3L: {tiempo3L} horas");
            Console.WriteLine($"Tiempo para llenar el balde de 5L: {tiempo5L} horas");

            //ejercicio extra
            Console.WriteLine("=== Cálculo de tiempo para llenar baldes ===");

            float tiempo1Litro = 90f;

            float tiempo3Litros = tiempo1Litro * 3;
            float tiempo5Litros = tiempo1Litro * 5;

            Console.WriteLine("\n--- Baldes conocidos (1L, 3L, 5L) ---");
            Console.WriteLine($"Tiempo para llenar 1 litro: {tiempo1Litro} minutos");
            Console.WriteLine($"Tiempo para llenar 3 litros: {tiempo3Litros} minutos");
            Console.WriteLine($"Tiempo para llenar 5 litros: {tiempo5Litros} minutos");

            Console.WriteLine("\n--- Baldes de tamaño ingresado por el usuario ---");
            Console.Write("Ingrese la cantidad de litros de un balde desconocido: ");
            float litros = float.Parse(Console.ReadLine());

            float tiempoDesconocido = tiempo1Litro * litros;

            Console.WriteLine($"Tiempo estimado para llenar {litros} litros: {tiempoDesconocido} minutos");

            // Ejercicio 6
            double tiempo7m = 5;

            Console.Write("Ingrese la altura que desea subir (en metros): ");

            double altura = double.Parse(Console.ReadLine());
            double tiempo = (altura * tiempo7m) / 7;

            Console.WriteLine($"Tiempo estimado para subir {altura} metros: {tiempo} horas");


        }

      }

    }
