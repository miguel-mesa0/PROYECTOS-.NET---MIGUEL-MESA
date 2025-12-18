using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Inventario.Models.Inventario;

public partial class AjustesInventarioDetalle
{
    [Key]
    public int Id { get; set; }

    public int AjusteInventarioId { get; set; }

    public int RepuestoId { get; set; }

    public int Cantidad { get; set; }

    [ForeignKey("AjusteInventarioId")]
    [InverseProperty("AjustesInventarioDetalles")]
    [ValidateNever]
    public virtual AjustesInventario AjusteInventario { get; set; } = null!;

    [ForeignKey("RepuestoId")]
    [InverseProperty("AjustesInventarioDetalles")]
    [ValidateNever]
    public virtual Repuesto Repuesto { get; set; } = null!;
}
