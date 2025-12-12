using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Models.Inventario;

public partial class Categoria
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [StringLength(250)]
    public string? Descripcion { get; set; }

    [InverseProperty("Categoria")]
    public virtual ICollection<Repuesto> Repuestos { get; set; } = new List<Repuesto>();
}
