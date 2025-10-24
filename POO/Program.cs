using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //----------Array---------
            //int[] numeros = new int[3];
            //for (int i = 0; i <3; i++)
            //{
            //    Console.WriteLine("ingrese numero "+ (i+ 1));
            //    numeros[i] = int.Parse(Console.ReadLine());
            //}
            //Console.WriteLine("\nnumeros ingresados");
            //foreach (var item in numeros)
            //{
            //    Console.WriteLine(item);
            //}
            //int suma =0;
            //for (int i = 0;i <3;i++)
            //{
            //    suma+= numeros[i];
            //}
            //Console.WriteLine("la suma de todos los datos es "+ suma);

            //--------------LISTA-------------
            //List<int> numeros = new List<int>();
            //numeros.Add(10);
            //numeros.Add(20);
            //numeros.Add(30);
            //numeros.Add(40);
            //numeros.Add(50);
            //Console.WriteLine("numeros en la lista ");
            //foreach (int item in numeros) 
            //{
            //    Console.WriteLine(item);
            //}
            ////acceder a un elemento por indice
            //int primeronumero = numeros[1];
            //Console.WriteLine("el numero en la lista "+primeronumero);
            ////modificar un elemento en la lista
            //numeros[2] = 50;
            //Console.WriteLine("numero modificado " + numeros[2]);
            ////insertar un elemento en una posicion especifica
            //numeros.Insert(2, 15);
            //Console.WriteLine("numero modificado"+ numeros[2]);
            ////eliminar un elementyo de la lista especifica

            List<string> productos = new List<string>();
            List<double> precios = new List<double>();

            int opcion = 0;

            while (opcion != 5)
            {
                Console.WriteLine("====== MENu DE PRODUCTOS ======");
                Console.WriteLine("1. Agregar producto");
                Console.WriteLine("2. Mostrar productos");
                Console.WriteLine("3. Actualizar producto");
                Console.WriteLine("4. Eliminar producto");
                Console.WriteLine("5. Salir");
                Console.Write("Elige una opción: ");
                opcion = int.Parse(Console.ReadLine());

                if (opcion == 1)
                {
                    Console.Write("Nombre del producto: ");
                    string nombre = Console.ReadLine();
                    Console.Write("Precio: ");
                    double precio = double.Parse(Console.ReadLine());
                    productos.Add(nombre);
                    precios.Add(precio);
                    Console.WriteLine("Producto agregado ");
                }
                else if (opcion == 2)
                {
                    Console.WriteLine("=== LISTA DE PRODUCTOS ===");
                    for (int i = 0; i < productos.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {productos[i]} - ${precios[i]}");
                    }
                }
                else if (opcion == 3)
                {
                    Console.Write("Número del producto a actualizar: ");
                    int num = int.Parse(Console.ReadLine()) - 1;
                    if (num >= 0 && num < productos.Count)
                    {
                        Console.Write("Nuevo nombre: ");
                        productos[num] = Console.ReadLine();
                        Console.Write("Nuevo precio: ");
                        precios[num] = double.Parse(Console.ReadLine());
                        Console.WriteLine("Producto actualizado ");
                    }
               
                }
                else if (opcion == 4)
                {
                    Console.Write("Número del producto a eliminar: ");
                    int num = int.Parse(Console.ReadLine()) - 1;
                    if (num >= 0 && num < productos.Count)
                    {
                        productos.RemoveAt(num);
                        precios.RemoveAt(num);
                        Console.WriteLine("Producto eliminado ");
                    }
                    
                }
                else if (opcion == 5)
                {
                    Console.WriteLine("Saliendo del programa... ");
                }
                
                Console.WriteLine();
            }
        }
    }
}
