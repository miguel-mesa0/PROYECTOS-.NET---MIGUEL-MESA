using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventario.Data;
using Inventario.Models.Inventario;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Controllers
{
    public class ComprasController : Controller
    {
        private readonly InventarioContext _context;

        // Convención: 1 = Compra, 2 = Venta, 3 = Ajuste
        private const byte TipoDocumentoCompra = 1;

        public ComprasController(InventarioContext context)
        {
            _context = context;
        }

        // GET: Compras
        public async Task<IActionResult> Index()
        {
            var compras = await _context.Compras
                .Include(c => c.Bodega)
                .Include(c => c.Proveedor)
                .OrderByDescending(c => c.Fecha)
                .ToListAsync();

            return View(compras);
        }

        // GET: Compras/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var compra = await _context.Compras
                .Include(c => c.Bodega)
                .Include(c => c.Proveedor)
                .Include(c => c.ComprasDetalles)
                    .ThenInclude(d => d.Repuesto)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (compra == null)
                return NotFound();

            return View(compra);
        }

        // GET: Compras/Create
        public async Task<IActionResult> Create()
        {
            ViewData["Bodegas"] = await _context.Bodegas.ToListAsync();
            ViewData["Proveedores"] = await _context.Proveedores.ToListAsync();
            ViewData["Repuestos"] = await _context.Repuestos.ToListAsync();

            return View();
        }

        // POST: Compras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Compra compra, List<ComprasDetalle> detalles)
        {
            // Filtrar solo líneas válidas (evita filas vacías desde la vista)
            var detallesValidos = (detalles ?? new List<ComprasDetalle>())
                .Where(d => d.RepuestoId != 0 && d.Cantidad > 0 && d.PrecioCostoUnitario > 0)
                .ToList();

            if (!detallesValidos.Any())
                ModelState.AddModelError(string.Empty, "Debe agregar al menos un repuesto con cantidad y costo.");

            if (!ModelState.IsValid)
            {
                ViewData["Bodegas"] = await _context.Bodegas.ToListAsync();
                ViewData["Proveedores"] = await _context.Proveedores.ToListAsync();
                ViewData["Repuestos"] = await _context.Repuestos.ToListAsync();
                return View(compra);
            }

            // Fecha
            compra.Fecha = DateTime.Now;

            // Totales
            compra.Subtotal = detallesValidos.Sum(d => d.Cantidad * d.PrecioCostoUnitario);
            compra.Impuestos = 0; // aquí luego puedes calcular IVA si quieres
            compra.Total = compra.Subtotal + compra.Impuestos;

            // 1) Guardar cabecera
            _context.Compras.Add(compra);
            await _context.SaveChangesAsync(); // para tener compra.Id

            // 2) Detalles + Stock + Movimientos
            foreach (var det in detallesValidos)
            {
                det.CompraId = compra.Id;
                _context.ComprasDetalles.Add(det);

                // STOCK POR BODEGA
                var stock = await _context.StocksRepuestosBodegas
                    .FirstOrDefaultAsync(s => s.RepuestoId == det.RepuestoId && s.BodegaId == compra.BodegaId);

                if (stock == null)
                {
                    stock = new StocksRepuestosBodega
                    {
                        RepuestoId = det.RepuestoId,
                        BodegaId = compra.BodegaId,
                        Cantidad = det.Cantidad
                    };
                    _context.StocksRepuestosBodegas.Add(stock);
                }
                else
                {
                    stock.Cantidad += det.Cantidad;
                }

                int stockResultado = stock.Cantidad;

                // MOVIMIENTO DE INVENTARIO (KARDEX)
                var movimiento = new MovimientosInventario
                {
                    RepuestoId = det.RepuestoId,
                    BodegaId = compra.BodegaId,
                    TipoDocumento = TipoDocumentoCompra,   // 1 = Compra
                    DocumentoId = compra.Id,               // Id de la compra
                    Fecha = compra.Fecha,
                    Cantidad = det.Cantidad,               // entrada (+)
                    StockResultado = stockResultado,
                    CostoUnitario = det.PrecioCostoUnitario
                };

                _context.MovimientosInventarios.Add(movimiento);

                // OPCIONAL: actualizar PrecioCostoPromedio del repuesto
                // var repuesto = await _context.Repuestos.FirstOrDefaultAsync(r => r.Id == det.RepuestoId);
                // if (repuesto != null)
                // {
                //     // lógica de costo promedio aquí, si la quieres implementar
                // }
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Compras/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var compra = await _context.Compras
                .Include(c => c.Bodega)
                .Include(c => c.Proveedor)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (compra == null)
                return NotFound();

            // Solo permitimos editar algunos campos de cabecera (no tocamos detalles ni stock)
            ViewData["Bodegas"] = await _context.Bodegas.ToListAsync();
            ViewData["Proveedores"] = await _context.Proveedores.ToListAsync();

            return View(compra);
        }

        // POST: Compras/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Compra compraEditada)
        {
            if (id != compraEditada.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                ViewData["Bodegas"] = await _context.Bodegas.ToListAsync();
                ViewData["Proveedores"] = await _context.Proveedores.ToListAsync();
                return View(compraEditada);
            }

            var compraOriginal = await _context.Compras.FirstOrDefaultAsync(c => c.Id == id);
            if (compraOriginal == null)
                return NotFound();

            // IMPORTANTE: aquí NO tocamos stock ni detalles.
            // Solo actualizamos datos "administrativos" de la compra.
            compraOriginal.ProveedorId = compraEditada.ProveedorId;
            compraOriginal.BodegaId = compraEditada.BodegaId;
            compraOriginal.NumeroDocumento = compraEditada.NumeroDocumento;
            // Si quieres permitir cambiar Fecha manualmente:
            // compraOriginal.Fecha = compraEditada.Fecha;

            try
            {
                _context.Update(compraOriginal);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompraExists(compraOriginal.Id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Compras/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var compra = await _context.Compras
                .Include(c => c.Bodega)
                .Include(c => c.Proveedor)
                .Include(c => c.ComprasDetalles)
                    .ThenInclude(d => d.Repuesto)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (compra == null)
                return NotFound();

            return View(compra);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var compra = await _context.Compras
                .Include(c => c.ComprasDetalles)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (compra == null)
                return RedirectToAction(nameof(Index));

            // 1) Revertir stock por cada detalle
            foreach (var det in compra.ComprasDetalles)
            {
                var stock = await _context.StocksRepuestosBodegas
                    .FirstOrDefaultAsync(s => s.RepuestoId == det.RepuestoId && s.BodegaId == compra.BodegaId);

                if (stock != null)
                {
                    stock.Cantidad -= det.Cantidad;
                    if (stock.Cantidad < 0)
                        stock.Cantidad = 0; // evitamos negativos; en un sistema real harías validaciones más estrictas

                    _context.StocksRepuestosBodegas.Update(stock);
                }
            }

            // 2) Borrar movimientos de inventario asociados a esta compra
            var movimientosCompra = await _context.MovimientosInventarios
                .Where(m => m.TipoDocumento == TipoDocumentoCompra && m.DocumentoId == compra.Id)
                .ToListAsync();

            _context.MovimientosInventarios.RemoveRange(movimientosCompra);

            // 3) Borrar detalles y cabecera
            _context.ComprasDetalles.RemoveRange(compra.ComprasDetalles);
            _context.Compras.Remove(compra);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool CompraExists(int id)
        {
            return _context.Compras.Any(e => e.Id == id);
        }
    }
}
