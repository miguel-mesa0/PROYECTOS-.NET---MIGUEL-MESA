using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Models.Inventario;

public partial class Compra
{
    [Key]
    public int Id { get; set; }

    public int ProveedorId { get; set; }

    public int BodegaId { get; set; }

    public DateTime Fecha { get; set; }

    [StringLength(50)]
    public string? NumeroDocumento { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Subtotal { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Impuestos { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Total { get; set; }

    [ForeignKey("BodegaId")]
    [InverseProperty("Compras")]
    [ValidateNever]
    public virtual Bodega Bodega { get; set; } = null!;

    [InverseProperty("Compra")]
    public virtual ICollection<ComprasDetalle> ComprasDetalles { get; set; } = new List<ComprasDetalle>();

    [ForeignKey("ProveedorId")]
    [InverseProperty("Compras")]
    [ValidateNever]
    public virtual Proveedore Proveedor { get; set; } = null!;
}
