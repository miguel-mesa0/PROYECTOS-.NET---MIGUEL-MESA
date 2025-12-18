using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace proyectoCrud.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public DateTime FechaRegistro { get; set; }

        // Relación: un cliente puede tener varias ventas
        public ICollection<Venta> Ventas { get; set; }
    }
}