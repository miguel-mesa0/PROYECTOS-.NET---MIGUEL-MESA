using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Models.Inventario;

public partial class Bodega
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [StringLength(200)]
    public string? Ubicacion { get; set; }

    public bool Activa { get; set; }

    [InverseProperty("Bodega")]
    public virtual ICollection<AjustesInventario> AjustesInventarios { get; set; } = new List<AjustesInventario>();

    [InverseProperty("Bodega")]
    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    [InverseProperty("Bodega")]
    public virtual ICollection<MovimientosInventario> MovimientosInventarios { get; set; } = new List<MovimientosInventario>();

    [InverseProperty("Bodega")]
    public virtual ICollection<StocksRepuestosBodega> StocksRepuestosBodegas { get; set; } = new List<StocksRepuestosBodega>();

    [InverseProperty("Bodega")]
    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
