using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Inventario.Models.Inventario;

public partial class Venta
{
    [Key]
    public int Id { get; set; }

    public int ClienteId { get; set; }

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
    [InverseProperty("Venta")]
    [ValidateNever]
    public virtual Bodega Bodega { get; set; } = null!;

    [ForeignKey("ClienteId")]
    [InverseProperty("Venta")]
    [ValidateNever]
    public virtual Cliente Cliente { get; set; } = null!;

    [InverseProperty("Venta")]
    public virtual ICollection<VentasDetalle> VentasDetalles { get; set; } = new List<VentasDetalle>();
}
