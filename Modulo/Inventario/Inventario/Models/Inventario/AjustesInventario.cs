using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Models.Inventario;

[Table("AjustesInventario")]
public partial class AjustesInventario
{
    [Key]
    public int Id { get; set; }

    public int BodegaId { get; set; }

    public DateTime Fecha { get; set; }

    [StringLength(250)]
    public string? Motivo { get; set; }

    [InverseProperty("AjusteInventario")]
    public virtual ICollection<AjustesInventarioDetalle> AjustesInventarioDetalles { get; set; } = new List<AjustesInventarioDetalle>();

    [ForeignKey("BodegaId")]
    [InverseProperty("AjustesInventarios")]
    [ValidateNever]
    public virtual Bodega Bodega { get; set; } = null!;
}
