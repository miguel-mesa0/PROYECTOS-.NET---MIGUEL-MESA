using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Models.Inventario;

public partial class Proveedore
{
    [Key]
    public int Id { get; set; }

    [StringLength(150)]
    public string Nombre { get; set; } = null!;

    [StringLength(20)]
    public string? NIT { get; set; }

    [StringLength(100)]
    public string? Telefono { get; set; }

    [StringLength(150)]
    public string? Email { get; set; }

    [StringLength(250)]
    public string? Direccion { get; set; }

    [InverseProperty("Proveedor")]
    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();
}
