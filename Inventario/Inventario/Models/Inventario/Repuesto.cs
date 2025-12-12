using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace Inventario.Models.Inventario;

public partial class Repuesto
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Codigo { get; set; } = null!;

    [StringLength(150)]
    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int CategoriaId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal PrecioCostoPromedio { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal MargenGananciaPorcentaje { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? DescuentoPorcentaje { get; set; }

    public bool Activo { get; set; }

    public int StockMinimoGlobal { get; set; }

    [InverseProperty("Repuesto")]
    public virtual ICollection<AjustesInventarioDetalle> AjustesInventarioDetalles { get; set; } = new List<AjustesInventarioDetalle>();

    [ForeignKey("CategoriaId")]
    [InverseProperty("Repuestos")]
    [ValidateNever]
    public virtual Categoria Categoria { get; set; } = null!;

    [InverseProperty("Repuesto")]
    public virtual ICollection<ComprasDetalle> ComprasDetalles { get; set; } = new List<ComprasDetalle>();

    [InverseProperty("Repuesto")]
    public virtual ICollection<MovimientosInventario> MovimientosInventarios { get; set; } = new List<MovimientosInventario>();

    [InverseProperty("Repuesto")]
    public virtual ICollection<StocksRepuestosBodega> StocksRepuestosBodegas { get; set; } = new List<StocksRepuestosBodega>();

    [InverseProperty("Repuesto")]
    public virtual ICollection<VentasDetalle> VentasDetalles { get; set; } = new List<VentasDetalle>();
}
