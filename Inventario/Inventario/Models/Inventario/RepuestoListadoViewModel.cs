using System;

namespace Inventario.Models.Inventario
{
    public class RepuestoListadoViewModel
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;

        public string? CategoriaNombre { get; set; }

        public decimal PrecioCostoPromedio { get; set; }
        public decimal MargenGananciaPorcentaje { get; set; }
        public decimal? DescuentoPorcentaje { get; set; }

        public bool Activo { get; set; }
        public int StockMinimoGlobal { get; set; }

        // 🔹 Stock total sumado en todas las bodegas (StocksRepuestosBodegas)
        public int StockTotal { get; set; }
    }
}
