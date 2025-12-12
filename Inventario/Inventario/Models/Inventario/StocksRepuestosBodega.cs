using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Models.Inventario;

[Index("RepuestoId", "BodegaId", Name = "UQ_Stock_Repuesto_Bodega", IsUnique = true)]
public partial class StocksRepuestosBodega
{
    [Key]
    public int Id { get; set; }

    public int RepuestoId { get; set; }

    public int BodegaId { get; set; }

    public int Cantidad { get; set; }

    [ForeignKey("BodegaId")]
    [InverseProperty("StocksRepuestosBodegas")]
    public virtual Bodega Bodega { get; set; } = null!;

    [ForeignKey("RepuestoId")]
    [InverseProperty("StocksRepuestosBodegas")]
    public virtual Repuesto Repuesto { get; set; } = null!;
}
