using System.ComponentModel.DataAnnotations;

namespace proyectoCrud.Models
{
    public class Venta
    {
        [Key]
        public int Id { get; set; }
        public DateTime FechaRegistro { get; set; }

        // Relaciones
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
    }
}