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
    public class VentasController : Controller
    {
        private readonly InventarioContext _context;

        // Convención: 1 = Compra, 2 = Venta, 3 = Ajuste
        private const byte TipoDocumentoVenta = 2;

        public VentasController(InventarioContext context)
        {
            _context = context;
        }

        // GET: Ventas
        public async Task<IActionResult> Index()
        {
            var ventas = await _context.Ventas
                .Include(v => v.Bodega)
                .Include(v => v.Cliente)
                .OrderByDescending(v => v.Fecha)
                .ToListAsync();

            return View(ventas);
        }

        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var venta = await _context.Ventas
                .Include(v => v.Bodega)
                .Include(v => v.Cliente)
                .Include(v => v.VentasDetalles)
                    .ThenInclude(d => d.Repuesto)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (venta == null)
                return NotFound();

            return View(venta);
        }

        // GET: Ventas/Create
        public async Task<IActionResult> Create()
        {
            ViewData["Bodegas"] = await _context.Bodegas.ToListAsync();
            ViewData["Clientes"] = await _context.Clientes.ToListAsync();

            // Solo repuestos activos
            ViewData["Repuestos"] = await _context.Repuestos
                .Where(r => r.Activo)
                .ToListAsync();

            return View();
        }

        // POST: Ventas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Venta venta, List<VentasDetalle> detalles)
        {
            var detallesValidos = (detalles ?? new List<VentasDetalle>())
                .Where(d => d.RepuestoId != 0 && d.Cantidad > 0 && d.PrecioVentaUnitario > 0)
                .ToList();

            if (!detallesValidos.Any())
                ModelState.AddModelError(string.Empty, "Debe agregar al menos un repuesto con cantidad y precio.");

            // Validar stock disponible por cada detalle
            if (venta.BodegaId == 0)
                ModelState.AddModelError("BodegaId", "Debe seleccionar una bodega.");

            foreach (var det in detallesValidos)
            {
                var stock = await _context.StocksRepuestosBodegas
                    .FirstOrDefaultAsync(s => s.RepuestoId == det.RepuestoId && s.BodegaId == venta.BodegaId);

                var disponible = stock?.Cantidad ?? 0;
                if (disponible < det.Cantidad)
                {
                    ModelState.AddModelError(string.Empty,
                        $"No hay stock suficiente para el repuesto ID {det.RepuestoId}. Disponible: {disponible}, solicitado: {det.Cantidad}.");
                }
            }

            if (!ModelState.IsValid)
            {
                ViewData["Bodegas"] = await _context.Bodegas.ToListAsync();
                ViewData["Clientes"] = await _context.Clientes.ToListAsync();
                ViewData["Repuestos"] = await _context.Repuestos.Where(r => r.Activo).ToListAsync();
                return View(venta);
            }

            // Fecha
            venta.Fecha = DateTime.Now;

            // Totales
            venta.Subtotal = detallesValidos.Sum(d => d.Cantidad * d.PrecioVentaUnitario);
            venta.Impuestos = 0; // aquí puedes aplicar IVA luego
            venta.Total = venta.Subtotal + venta.Impuestos;

            // 1) Guardar cabecera
            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync(); // obtiene Id

            // 2) Detalles + Stock - Movimientos
            foreach (var det in detallesValidos)
            {
                det.VentaId = venta.Id;
                _context.VentasDetalles.Add(det);

                // STOCK POR BODEGA
                var stock = await _context.StocksRepuestosBodegas
                    .FirstOrDefaultAsync(s => s.RepuestoId == det.RepuestoId && s.BodegaId == venta.BodegaId);

                if (stock == null || stock.Cantidad < det.Cantidad)
                {
                    // Esto no debería pasar por la validación anterior, pero por si acaso:
                    throw new InvalidOperationException("Stock insuficiente al procesar la venta.");
                }

                stock.Cantidad -= det.Cantidad;
                _context.StocksRepuestosBodegas.Update(stock);

                int stockResultado = stock.Cantidad;

                // Obtener costo para kardex (usamos PrecioCostoPromedio del repuesto)
                var repuesto = await _context.Repuestos.FirstOrDefaultAsync(r => r.Id == det.RepuestoId);
                decimal costoUnitarioKardex = repuesto?.PrecioCostoPromedio ?? 0;

                // MOVIMIENTO INVENTARIO (salida negativa)
                var movimiento = new MovimientosInventario
                {
                    RepuestoId = det.RepuestoId,
                    BodegaId = venta.BodegaId,
                    TipoDocumento = TipoDocumentoVenta, // 2 = Venta
                    DocumentoId = venta.Id,
                    Fecha = venta.Fecha,
                    Cantidad = -det.Cantidad,          // salida
                    StockResultado = stockResultado,
                    CostoUnitario = costoUnitarioKardex
                };

                _context.MovimientosInventarios.Add(movimiento);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Ventas/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var venta = await _context.Ventas
                .Include(v => v.Bodega)
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (venta == null)
                return NotFound();

            ViewData["Bodegas"] = await _context.Bodegas.ToListAsync();
            ViewData["Clientes"] = await _context.Clientes.ToListAsync();

            return View(venta);
        }

        // POST: Ventas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Venta ventaEditada)
        {
            if (id != ventaEditada.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                ViewData["Bodegas"] = await _context.Bodegas.ToListAsync();
                ViewData["Clientes"] = await _context.Clientes.ToListAsync();
                return View(ventaEditada);
            }

            var ventaOriginal = await _context.Ventas.FirstOrDefaultAsync(v => v.Id == id);
            if (ventaOriginal == null)
                return NotFound();

            // Solo actualizamos información administrativa
            ventaOriginal.BodegaId = ventaEditada.BodegaId;
            ventaOriginal.ClienteId = ventaEditada.ClienteId;
            ventaOriginal.NumeroDocumento = ventaEditada.NumeroDocumento;
            // ventaOriginal.Fecha = ventaEditada.Fecha; // si quieres permitir cambiar la fecha

            try
            {
                _context.Update(ventaOriginal);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VentaExists(ventaOriginal.Id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Ventas/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var venta = await _context.Ventas
                .Include(v => v.Bodega)
                .Include(v => v.Cliente)
                .Include(v => v.VentasDetalles)
                    .ThenInclude(d => d.Repuesto)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (venta == null)
                return NotFound();

            return View(venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venta = await _context.Ventas
                .Include(v => v.VentasDetalles)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (venta == null)
                return RedirectToAction(nameof(Index));

            // 1) Revertir stock (sumar lo que se había restado)
            foreach (var det in venta.VentasDetalles)
            {
                var stock = await _context.StocksRepuestosBodegas
                    .FirstOrDefaultAsync(s => s.RepuestoId == det.RepuestoId && s.BodegaId == venta.BodegaId);

                if (stock != null)
                {
                    stock.Cantidad += det.Cantidad;
                    _context.StocksRepuestosBodegas.Update(stock);
                }
            }

            // 2) Borrar movimientos asociados a esta venta
            var movimientosVenta = await _context.MovimientosInventarios
                .Where(m => m.TipoDocumento == TipoDocumentoVenta && m.DocumentoId == venta.Id)
                .ToListAsync();

            _context.MovimientosInventarios.RemoveRange(movimientosVenta);

            // 3) Borrar detalles y cabecera
            _context.VentasDetalles.RemoveRange(venta.VentasDetalles);
            _context.Ventas.Remove(venta);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool VentaExists(int id)
        {
            return _context.Ventas.Any(e => e.Id == id);
        }
    }
}
