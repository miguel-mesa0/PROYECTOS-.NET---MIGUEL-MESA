using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Models.Inventario;

public partial class Cliente
{
    [Key]
    public int Id { get; set; }

    [StringLength(150)]
    public string Nombre { get; set; } = null!;

    [StringLength(20)]
    public string? Documento { get; set; }

    [StringLength(100)]
    public string? Telefono { get; set; }

    [StringLength(150)]
    public string? Email { get; set; }

    [InverseProperty("Cliente")]
    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
