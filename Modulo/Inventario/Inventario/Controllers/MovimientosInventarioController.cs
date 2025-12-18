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
    public class MovimientosInventarioController : Controller
    {
        private readonly InventarioContext _context;

        public MovimientosInventarioController(InventarioContext context)
        {
            _context = context;
        }

        // ===============================================================
        // INDEX = KARDEX
        // GET: /MovimientosInventario
        // ===============================================================
        [HttpGet]
        public async Task<IActionResult> Index(int? repuestoId, int? bodegaId, DateTime? desde, DateTime? hasta)
        {
            // 1) Cargar combos
            var repuestos = await _context.Repuestos
                .OrderBy(r => r.Nombre)
                .ToListAsync();

            var bodegas = await _context.Bodegas
                .OrderBy(b => b.Nombre)
                .ToListAsync();

            ViewBag.Repuestos = repuestos;
            ViewBag.Bodegas = bodegas;

            ViewBag.RepuestoIdSel = repuestoId;
            ViewBag.BodegaIdSel = bodegaId;
            ViewBag.Desde = desde;
            ViewBag.Hasta = hasta;

            // 2) Query base
            var query = _context.MovimientosInventarios
                .Include(m => m.Repuesto)
                .Include(m => m.Bodega)
                .AsQueryable();

            if (repuestoId.HasValue)
            {
                query = query.Where(m => m.RepuestoId == repuestoId.Value);
            }

            if (bodegaId.HasValue)
            {
                query = query.Where(m => m.BodegaId == bodegaId.Value);
            }

            if (desde.HasValue)
            {
                query = query.Where(m => m.Fecha >= desde.Value);
            }

            if (hasta.HasValue)
            {
                var hastaFin = hasta.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(m => m.Fecha <= hastaFin);
            }

            // 3) Saldo inicial (solo si hay repuesto + bodega + desde)
            int saldoInicial = 0;

            if (repuestoId.HasValue && bodegaId.HasValue && desde.HasValue)
            {
                saldoInicial = await _context.MovimientosInventarios
                    .Where(m =>
                        m.RepuestoId == repuestoId.Value &&
                        m.BodegaId == bodegaId.Value &&
                        m.Fecha < desde.Value)
                    .SumAsync(m => (int?)m.Cantidad) ?? 0;
            }

            ViewBag.SaldoInicial = saldoInicial;

            // 4) Obtener movimientos del rango
            var movimientos = await query
                .OrderBy(m => m.Fecha)
                .ThenBy(m => m.Id)
                .ToListAsync();

            return View(movimientos);
        }

        // ===============================================================
        // DETAILS (opcional: ver un movimiento puntual del kardex)
        // GET: /MovimientosInventario/Details/5
        // ===============================================================
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var movimiento = await _context.MovimientosInventarios
                .Include(m => m.Repuesto)
                .Include(m => m.Bodega)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movimiento == null)
                return NotFound();

            return View(movimiento);
        }
    }
}
