using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TALLER_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
//            //PRIMER EJERCICIO-----------------------------------------------------------------------
//            Console.Write("Ingrese el monto del préstamo: ");
//            double monto = double.Parse(Console.ReadLine());
//            double tasa = 0.05;

//            double interesAnual = monto * tasa;
//            double interesTrimestre = interesAnual / 4;
//            double interesMensual = interesAnual / 12;
//            double totalPagar = monto + (interesAnual * 5);

//            Console.WriteLine($"Interés en un año: ${interesAnual}");
//            Console.WriteLine($"Interés en el tercer trimestre: ${interesTrimestre}");
//            Console.WriteLine($"Interés en el primer mes: ${interesMensual}");
//            Console.WriteLine($"Total a pagar con intereses (5 años): ${totalPagar}");

//            //SEGUNDO EJERCICIO-----------------------------------------------------------------------
//            Console.Write("Ingrese el salario del empleado: ");
//            double salario = double.Parse(Console.ReadLine());

//            Console.Write("Ingrese el valor del ahorro mensual programado: ");
//            double ahorro = double.Parse(Console.ReadLine());

//            double salud = salario * 0.125;
//            double pension = salario * 0.16;
//            double total = salario - salud - pension - ahorro;

//            Console.WriteLine($"\n--- Colilla de Pago ---");
//            Console.WriteLine($"Salario: ${salario}");
//            Console.WriteLine($"Ahorro mensual: ${ahorro}");
//            Console.WriteLine($"Deducción Salud (12.5%): ${salud}");
//            Console.WriteLine($"Deducción Pensión (16%): ${pension}");
//            Console.WriteLine($"Total a recibir: ${total}");

//            //TERCERO EJERCICIO-----------------------------------------------------------------------
//        public string Nombre;
//        public int Edad;
//        public string Genero;

//        public Persona(string nombre, int edad, string genero)
//        {
//            Nombre = nombre;
//            Edad = edad;
//            Genero = genero;
//        }

//        public void ImprimirDetalles()
//        {
//            Console.WriteLine($"Nombre: {Nombre}, Edad: {Edad}, Género: {Genero}");
//        }

//        public void CalcularEdadEnDias()
//        {
//            Console.WriteLine($"Edad en días: {Edad * 365}");
//        }
//    }

//    class Programa3
//    {
//        static void Main()
//        {
//            Console.Write("Nombre: ");
//            string nombre = Console.ReadLine();
//            Console.Write("Edad: ");
//            int edad = int.Parse(Console.ReadLine());
//            Console.Write("Género (F/M): ");
//            string genero = Console.ReadLine();

//            Persona persona = new Persona(nombre, edad, genero);

//            int opcion;
//            do
//            {
//                Console.WriteLine("\n1. Imprimir detalles\n2. Calcular edad en días\n3. Salir");
//                opcion = int.Parse(Console.ReadLine());

//                switch (opcion)
//                {
//                    case 1: persona.ImprimirDetalles(); break;
//                    case 2: persona.CalcularEdadEnDias(); break;
//                }
//            } while (opcion != 3);
//        }
//        //CUARTO EJERCICIO-----------------------------------------------------------------------
//        public string Titulo, Autor, Editorial;
//        public int AnioPublicacion;

//        public Libro(string titulo, string autor, string editorial, int anio)
//        {
//            Titulo = titulo; Autor = autor; Editorial = editorial; AnioPublicacion = anio;
//        }
//    }

//    class Biblioteca
//    {
//        List<Libro> libros = new List<Libro>();

//        public void AgregarLibro(Libro libro)
//        {
//            libros.Add(libro);
//        }

//        public void ListarLibros()
//        {
//            foreach (var l in libros)
//                Console.WriteLine($"{l.Titulo} - {l.Autor} - {l.Editorial} - {l.AnioPublicacion}");
//        }

//        public void BuscarLibro(string titulo)
//        {
//            foreach (var l in libros)
//                if (l.Titulo.ToLower() == titulo.ToLower())
//                    Console.WriteLine($"Encontrado: {l.Titulo} de {l.Autor}");
//        }
//    }

//    class Programa4
//    {
//        static void Main()
//        {
//            Biblioteca biblio = new Biblioteca();
//            int opcion;

//            do
//            {
//                Console.WriteLine("\n1. Agregar libro\n2. Listar libros\n3. Buscar libro\n4. Salir");
//                opcion = int.Parse(Console.ReadLine());

//                switch (opcion)
//                {
//                    case 1:
//                        Console.Write("Título: "); string t = Console.ReadLine();
//                        Console.Write("Autor: "); string a = Console.ReadLine();
//                        Console.Write("Editorial: "); string e = Console.ReadLine();
//                        Console.Write("Año: "); int an = int.Parse(Console.ReadLine());
//                        biblio.AgregarLibro(new Libro(t, a, e, an));
//                        break;
//                    case 2: biblio.ListarLibros(); break;
//                    case 3:
//                        Console.Write("Ingrese el título a buscar: ");
//                        biblio.BuscarLibro(Console.ReadLine());
//                        break;
//                }
//            } while (opcion != 4);
//        }
//        //QUINTO EJERCICIO-----------------------------------------------------------------------
//        string[] programas = { "Ing. Sistemas", "Psicología", "Economía", "Comunicación Social", "Administración" };
//        int[] creditos = { 20, 16, 18, 18, 20 };
//        double[] descuentos = { 0.18, 0.12, 0.10, 0.05, 0.15 };
//        int[] estudiantes = new int[5];

//        double valorCredito = 200000;
//        double totalSinDesc = 0, totalDesc = 0;
//        int totalCreditos = 0;

//        Console.Write("Ingrese cantidad de estudiantes: ");
//        int n = int.Parse(Console.ReadLine());

//        for (int i = 0; i<n; i++)
//        {
//            Console.WriteLine("\nSeleccione programa (1-5):");
//            for (int j = 0; j<programas.Length; j++)
//                Console.WriteLine($"{j + 1}. {programas[j]}");

//            int opcion = int.Parse(Console.ReadLine()) - 1;
//        Console.Write("Forma de pago (1. Efectivo / 2. En línea): ");
//            int pago = int.Parse(Console.ReadLine());

//        double valor = creditos[opcion] * valorCredito;
//        double desc = (pago == 1) ? valor * descuentos[opcion] : 0;

//        totalSinDesc += valor;
//            totalDesc += desc;
//            totalCreditos += creditos[opcion];
//            estudiantes[opcion]++;
//        }

//    Console.WriteLine("\n--- Resultados ---");
//        for (int i = 0; i<programas.Length; i++)
//            Console.WriteLine($"{programas[i]}: {estudiantes[i]} estudiantes");
//        Console.WriteLine($"Total créditos: {totalCreditos}");
//        Console.WriteLine($"Valor sin descuentos: ${totalSinDesc}");
//        Console.WriteLine($"Total descuentos: ${totalDesc}");
//        Console.WriteLine($"Valor neto: ${totalSinDesc - totalDesc}");
//    }
////SEXTO EJERCICIO-----------------------------------------------------------------------
//Console.Write("Ingrese el número de empleados: ");
//int empleados = int.Parse(Console.ReadLine());

//double pagoBasico = 500000;
//for (int i = 1; i <= empleados; i++)
//{
//    Console.WriteLine($"\nEmpleado {i}");
//    Console.Write("Ingrese número de ventas realizadas: ");
//    int ventas = int.Parse(Console.ReadLine());

//    int v1 = 0, v2 = 0, v3 = 0;
//    double totalVenta = 0;

//    for (int j = 1; j <= ventas; j++)
//    {
//        Console.Write($"Valor de la venta {j}: ");
//        double valor = double.Parse(Console.ReadLine());
//        totalVenta += valor;

//        if (valor <= 300000) v1++;
//        else if (valor < 800000) v2++;
//        else v3++;
//    }

//    double bono = 0;
//    if (totalVenta >= 800000) bono = totalVenta * 0.10;
//    else if (totalVenta >= 400001 && totalVenta <= 800000) bono = totalVenta * 0.05;
//    else if (totalVenta >= 400000) bono = totalVenta * 0.03;

//    double totalPagar = pagoBasico + bono;

//    Console.WriteLine($"\nVentas <= 300.000: {v1}");
//    Console.WriteLine($"Ventas entre 300.001 y 800.000: {v2}");
//    Console.WriteLine($"Ventas >= 800.000: {v3}");
//    Console.WriteLine($"Total ventas: ${totalVenta}");
//    Console.WriteLine($"Bonificación: ${bono}");
//    Console.WriteLine($"Pago total al empleado: ${totalPagar}");

//    //SEPTIMO EJERCICIO-----------------------------------------------------------------------
//    Console.Write("Ingrese cantidad de conductores: ");
//    int n = int.Parse(Console.ReadLine());

//    int menores30 = 0, hombres = 0, mujeres = 0, hombres12a30 = 0, fueraBogota = 0;

//    for (int i = 0; i < n; i++)
//    {
//        Console.WriteLine($"\nConductor {i + 1}");
//        Console.Write("Año de nacimiento: ");
//        int anio = int.Parse(Console.ReadLine());
//        Console.Write("Sexo (1: Femenino / 2: Masculino): ");
//        int sexo = int.Parse(Console.ReadLine());
//        Console.Write("Registro del carro (1: Bogotá / 2: Otras ciudades): ");
//        int registro = int.Parse(Console.ReadLine());

//        int edad = 2025 - anio;

//        if (edad < 30) menores30++;
//        if (sexo == 2) hombres++; else mujeres++;
//        if (sexo == 2 && edad >= 12 && edad <= 30) hombres12a30++;
//        if (registro == 2) fueraBogota++;
//    }

//    Console.WriteLine("\n--- Resultados ---");
//    Console.WriteLine($"Porcentaje menores de 30: {(double)menores30 / n * 100}%");
//    Console.WriteLine($"Porcentaje hombres: {(double)hombres / n * 100}%");
//    Console.WriteLine($"Porcentaje mujeres: {(double)mujeres / n * 100}%");
//    Console.WriteLine($"Porcentaje hombres entre 12 y 30: {(double)hombres12a30 / n * 100}%");
//    Console.WriteLine($"Porcentaje carros fuera de Bogotá: {(double)fueraBogota / n * 100}%");

//    //OCTAVO EJERCICIO-----------------------------------------------------------------------

//    Console.Write("Ingrese cantidad de empleados: ");
//    int n = int.Parse(Console.ReadLine());

//    int[] empleadosMes = new int[12];
//    int totalEdades = 0;
//    double bono = 150000;
//    double totalBonos = 0;

//    for (int i = 0; i < n; i++)
//    {
//        Console.WriteLine($"\nEmpleado {i + 1}");
//        Console.Write("Año de nacimiento: ");
//        int anio = int.Parse(Console.ReadLine());
//        Console.Write("Mes de cumpleaños (1-12): ");
//        int mes = int.Parse(Console.ReadLine());

//        int edad = 2025 - anio;
//        totalEdades += edad;

//        if (edad >= 18 && edad < 50)
//        {
//            empleadosMes[mes - 1]++;
//            totalBonos += bono;
//        }
//    }

//    double promedioEdad = (double)totalEdades / n;
//    Console.WriteLine($"\nPromedio de edades: {promedioEdad:F2} años");
//    Console.WriteLine("\n--- Lista de Bonos por Mes ---");

//    for (int i = 0; i < 12; i++)
//    {
//        if (empleadosMes[i] > 0)
//        {
//            Console.WriteLine($"Mes {i + 1}: {empleadosMes[i]} empleados - Bonos ${empleadosMes[i] * bono}");
//        }
//    }

//    Console.WriteLine($"\nTotal dinero en bonos: ${totalBonos}");
//    //NOVENO EJERCICIO-----------------------------------------------------------------------


//    Console.WriteLine("Carga de camiones (20 diarios máximo)");

//    for (int i = 1; i <= 20; i++)
//    {
//        Console.Write($"\nCamión {i}: Ingrese capacidad en litros: ");
//        double capacidad = double.Parse(Console.ReadLine());
//        double cargaActual = 0;
//        int tanques = 0;

//        while (true)
//        {
//            Console.Write("Ingrese litros del tanque (0 para terminar): ");
//            double tanque = double.Parse(Console.ReadLine());
//            if (tanque == 0) break;

//            if (cargaActual + tanque <= capacidad)
//            {
//                cargaActual += tanque;
//                tanques++;
//            }
//            else
//            {
//                Console.WriteLine("Capacidad alcanzada. Despachar camión.");
//                break;
//            }
//        }

//        Console.WriteLine($"Camión {i} cargado con {tanques} tanques ({cargaActual} litros).");
//    }
//    Console.WriteLine("\nTodos los camiones fueron despachados correctamente.");

}
    }
}            
