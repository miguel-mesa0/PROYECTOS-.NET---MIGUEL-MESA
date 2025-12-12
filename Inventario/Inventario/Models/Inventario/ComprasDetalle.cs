using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Models.Inventario;

public partial class ComprasDetalle
{
    [Key]
    public int Id { get; set; }

    public int CompraId { get; set; }

    public int RepuestoId { get; set; }

    public int Cantidad { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal PrecioCostoUnitario { get; set; }

    [ForeignKey("CompraId")]
    [InverseProperty("ComprasDetalles")]
    [ValidateNever]
    public virtual Compra Compra { get; set; } = null!;

    [ForeignKey("RepuestoId")]
    [InverseProperty("ComprasDetalles")]
    [ValidateNever]
    public virtual Repuesto Repuesto { get; set; } = null!;
}
