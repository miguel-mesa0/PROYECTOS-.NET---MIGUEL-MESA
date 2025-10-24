using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructurasDeControl_3147912
{
    internal class auto1
    {
        //atributos
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Año { get; set; }

        //constructor
        public auto1(string marca, string modelo, int año)
        {
            Marca = marca;
            Modelo = modelo;
            Año = año;
        }


      //metodo para mostrar informacion del auto
      public void MostrarInfo()
        {
            Console.WriteLine($"marca:{Marca} | modelo:{Modelo} | Año:{Año}");
        }


    }
}
