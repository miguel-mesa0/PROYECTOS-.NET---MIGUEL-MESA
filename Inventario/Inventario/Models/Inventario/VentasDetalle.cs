using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Inventario.Models.Inventario;

public partial class VentasDetalle
{
    [Key]
    public int Id { get; set; }

    public int VentaId { get; set; }

    public int RepuestoId { get; set; }

    public int Cantidad { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal PrecioVentaUnitario { get; set; }

    [ForeignKey("RepuestoId")]
    [InverseProperty("VentasDetalles")]
    [ValidateNever]
    public virtual Repuesto Repuesto { get; set; } = null!;

    [ForeignKey("VentaId")]
    [InverseProperty("VentasDetalles")]
    [ValidateNever]
    public virtual Venta Venta { get; set; } = null!;
}
