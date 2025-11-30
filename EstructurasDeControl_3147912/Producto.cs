using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructurasDeControl_3147912
{
    internal class Producto
    {
        public int Id { get; set; } 
        public string Nombre { get; set; }    
        public decimal Precio { get; set; }

        public Producto(int id, string nombre, decimal precio)
        {
            Id = id;
            Nombre = nombre;
            Precio = precio;
        }

        public class ProductoCrud
        {
            public List<Producto> Productos = new List<Producto>();
            public int siguienteId = 1;
        }
        public void AgregarProducto()
        {
            Console.WriteLine("ingrese el nombre");
            string nombre = Console.ReadLine();
            Console.WriteLine("ingrese el precio ");
            decimal precio = decimal.Parse(Console.ReadLine());

            Producto nuevoProducto = new Producto(siguienteId++, nombre, precio);
            productos.Add(nuevoProducto);
            Console.WriteLine("producto agregado exitosamente");

            public void MostrarProductos()
            {
                Console.WriteLine("Lista de productos");
                foreach (var producto in productos)
                {
                    Console.WriteLine("id" + producto.Id + "nombre" + producto.Nombre + "precio" + producto.precio);
                }
            }
            public void ActualizarProducto()
            {
                Console.WriteLine("Ingrese el id del producto a actualizar");
                int idActualizar=int.Parse(Console.ReadLine());
                var producto = productos.Find(p => p.ID == idActualizar);
                if (producto != null)
                {
                    Console.WriteLine("Ingrese el nuevo nombre del producto");
                    producto.Nombre=Console.ReadLine();
                    Console.WriteLine("ingrese el nuevo precio del producto");
                    producto.precio=decimal.Parse(Console.ReadLine());
                    Console.WriteLine("actualizado exitosamente");

                }
                else
                {
                    Console.WriteLine("no");
                }
            }

            public void EliminarProducto()
            {
                Console.WriteLine("Ingrese el id del producto a actualizar");
                int idActualizar = int.Parse(Console.ReadLine());
                var producto = productos.Find(p => p.ID == idEliminar);
                if (producto != null)
                {
                   productos.Remove

                }
                else
                {
                    Console.WriteLine("no");
                }
            }
        }
    }
}
