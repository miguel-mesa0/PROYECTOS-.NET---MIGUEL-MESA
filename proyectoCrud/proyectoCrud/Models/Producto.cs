using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace proyectoCrud.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int Stock { get; set; }
        public DateTime FechaCreacion { get; set; }

        // Relación: un producto puede estar en varias ventas
        public ICollection<Venta> Ventas { get; set; }
    }
}