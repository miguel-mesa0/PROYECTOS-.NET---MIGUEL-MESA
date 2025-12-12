using System;
using System.Linq;
using System.Threading.Tasks;
using Inventario.Data;
using Inventario.Models.Inventario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Controllers
{
    [Authorize]
    public class AjustesInventariosController : Controller
    {
        private readonly InventarioContext _context;

        // Convención: 1 = Compra, 2 = Venta, 3 = Ajuste
        private const byte TipoDocumentoAjuste = 3;

        public AjustesInventariosController(InventarioContext context)
        {
            _context = context;
        }

        // ===============================================================
        // LISTADO
        // ===============================================================
        public async Task<IActionResult> Index()
        {
            var ajustes = await _context.AjustesInventarios
                .Include(a => a.Bodega)
                .Include(a => a.AjustesInventarioDetalles)
                .OrderByDescending(a => a.Fecha)
                .ToListAsync();

            return View(ajustes);
        }

        // ===============================================================
        // DETALLES
        // ===============================================================
        public async Task<IActionResult> Details(int id)
        {
            var ajuste = await _context.AjustesInventarios
                .Include(a => a.Bodega)
                .Include(a => a.AjustesInventarioDetalles)
                    .ThenInclude(d => d.Repuesto)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (ajuste == null)
                return NotFound();

            return View(ajuste);
        }

        // ===============================================================
        // CREAR (GET)
        // ===============================================================
        public async Task<IActionResult> Create()
        {
            ViewData["Bodegas"] = await _context.Bodegas.ToListAsync();
            ViewData["Repuestos"] = await _context.Repuestos
                .OrderBy(r => r.Nombre)
                .ToListAsync();

            var modelo = new AjustesInventario
            {
                Fecha = DateTime.Now
            };

            return View(modelo);
        }

        // ===============================================================
        // CREAR (POST)
        // ===============================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AjustesInventario ajuste, List<AjustesInventarioDetalle> detalles)
        {
            // Filtrar detalles válidos (permitir cantidades positivas o negativas)
            var detallesValidos = (detalles ?? new List<AjustesInventarioDetalle>())
                .Where(d => d.RepuestoId != 0 && d.Cantidad != 0)
                .ToList();

            if (!detallesValidos.Any())
            {
                ModelState.AddModelError(string.Empty, "Debe agregar al menos un repuesto con una cantidad distinta de cero.");
            }

            if (!ModelState.IsValid)
            {
                ViewData["Bodegas"] = await _context.Bodegas.ToListAsync();
                ViewData["Repuestos"] = await _context.Repuestos
                    .OrderBy(r => r.Nombre)
                    .ToListAsync();

                return View(ajuste);
            }

            // Fecha del ajuste
            ajuste.Fecha = DateTime.Now;

            try
            {
                // 1) Guardar la cabecera
                _context.AjustesInventarios.Add(ajuste);
                await _context.SaveChangesAsync(); // Necesario para obtener el Id del ajuste

                // 2) Procesar detalles, actualizar stocks y kardex
                foreach (var det in detallesValidos)
                {
                    det.AjusteInventarioId = ajuste.Id;
                    _context.AjustesInventarioDetalles.Add(det);

                    // Verificar stock por bodega y repuesto
                    var stock = await _context.StocksRepuestosBodegas
                        .FirstOrDefaultAsync(s => s.RepuestoId == det.RepuestoId && s.BodegaId == ajuste.BodegaId);

                    if (stock == null)
                    {
                        stock = new StocksRepuestosBodega
                        {
                            RepuestoId = det.RepuestoId,
                            BodegaId = ajuste.BodegaId,
                            Cantidad = 0
                        };
                        _context.StocksRepuestosBodegas.Add(stock);
                        await _context.SaveChangesAsync();
                    }

                    // Actualizar stock
                    stock.Cantidad += det.Cantidad;
                    _context.StocksRepuestosBodegas.Update(stock);

                    // Costo unitario del repuesto
                    var repuesto = await _context.Repuestos.FindAsync(det.RepuestoId);
                    var costoUnitario = repuesto?.PrecioCostoPromedio ?? 0;

                    // Registrar en Kardex
                    var movimiento = new MovimientosInventario
                    {
                        RepuestoId = det.RepuestoId,
                        BodegaId = ajuste.BodegaId,
                        TipoDocumento = TipoDocumentoAjuste, // Ajuste
                        DocumentoId = ajuste.Id,
                        Fecha = ajuste.Fecha,
                        Cantidad = det.Cantidad,
                        StockResultado = stock.Cantidad,
                        CostoUnitario = costoUnitario
                    };

                    _context.MovimientosInventarios.Add(movimiento);
                }

                await _context.SaveChangesAsync(); // Guardar todo

                return RedirectToAction(nameof(Index)); // Redirigir al índice después de guardar
            }
            catch (Exception ex)
            {
                // En caso de error, muestra el mensaje
                ModelState.AddModelError(string.Empty, $"Error al guardar el ajuste: {ex.Message}");
            }

            // Si algo falla, vuelve a la vista de creación con el modelo
            ViewData["Bodegas"] = await _context.Bodegas.ToListAsync();
            ViewData["Repuestos"] = await _context.Repuestos.OrderBy(r => r.Nombre).ToListAsync();
            return View(ajuste);
        }

        // ===============================================================
        // ELIMINAR (GET)
        // ===============================================================
        public async Task<IActionResult> Delete(int id)
        {
            var ajuste = await _context.AjustesInventarios
                .Include(a => a.Bodega)
                .Include(a => a.AjustesInventarioDetalles)
                    .ThenInclude(d => d.Repuesto)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (ajuste == null)
                return NotFound();

            return View(ajuste);
        }

        // ===============================================================
        // ELIMINAR (POST)
        // ===============================================================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ajuste = await _context.AjustesInventarios
                .Include(a => a.AjustesInventarioDetalles)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (ajuste == null)
                return RedirectToAction(nameof(Index));

            // 1) Revertir stocks
            foreach (var det in ajuste.AjustesInventarioDetalles)
            {
                var stock = await _context.StocksRepuestosBodegas
                    .FirstOrDefaultAsync(s =>
                        s.RepuestoId == det.RepuestoId &&
                        s.BodegaId == ajuste.BodegaId);

                if (stock != null)
                {
                    // Revertimos el ajuste (restamos lo que sumamos y viceversa)
                    stock.Cantidad -= det.Cantidad;
                    _context.StocksRepuestosBodegas.Update(stock);
                }
            }

            // 2) Eliminar movimientos de kardex de este ajuste
            var movimientos = await _context.MovimientosInventarios
                .Where(m => m.TipoDocumento == TipoDocumentoAjuste && m.DocumentoId == ajuste.Id)
                .ToListAsync();

            _context.MovimientosInventarios.RemoveRange(movimientos);

            // 3) Eliminar detalles y cabecera
            _context.AjustesInventarioDetalles.RemoveRange(ajuste.AjustesInventarioDetalles);
            _context.AjustesInventarios.Remove(ajuste);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
