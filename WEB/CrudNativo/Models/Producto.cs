using System.ComponentModel.DataAnnotations;
using System.Data;

namespace CrudNativo.Models
{
    public class Producto
    {
        [Key]
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}
