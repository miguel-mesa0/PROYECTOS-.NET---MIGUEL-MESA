    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Inventario.Models.Inventario;

[Table("MovimientosInventario")]
public partial class MovimientosInventario
{
    [Key]
    public int Id { get; set; }

    public int RepuestoId { get; set; }

    public int BodegaId { get; set; }

    public byte TipoDocumento { get; set; }

    public int DocumentoId { get; set; }

    public DateTime Fecha { get; set; }

    public int Cantidad { get; set; }

    public int StockResultado { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal CostoUnitario { get; set; }

    [ForeignKey("BodegaId")]
    [InverseProperty("MovimientosInventarios")]
    [ValidateNever]

    public virtual Bodega Bodega { get; set; } = null!;

    [ForeignKey("RepuestoId")]
    [InverseProperty("MovimientosInventarios")]
    [ValidateNever]
    public virtual Repuesto Repuesto { get; set; } = null!;
}
