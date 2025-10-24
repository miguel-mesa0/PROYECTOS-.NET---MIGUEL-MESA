using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructurasDeControl_3147912
{
    internal class Estudiante
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }

        public Estudiante(string nombre, int edad)
        {
            Nombre = nombre;
            Edad = edad;
        }

        //metodo
        public void VerificarEdad()
        {
            if(Edad >= 18)
            {
                Console.WriteLine(Nombre + "es mayor ");
            }
            else
            {
                Console.WriteLine(Nombre +"es menor")
            }
        }
    }
}
