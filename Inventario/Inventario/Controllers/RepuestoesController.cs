using System;
using System.Linq;
using System.Threading.Tasks;
using Inventario.Data;
using Inventario.Models.Inventario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Controllers
{
    [Authorize]
    public class RepuestoesController : Controller
    {
        private readonly InventarioContext _context;

        public RepuestoesController(InventarioContext context)
        {
            _context = context;
        }

        // ===============================================================
        // LISTADO (Index) - con stock total por repuesto
        // ===============================================================
        public async Task<IActionResult> Index()
        {
            var repuestos = await _context.Repuestos
                .Include(r => r.Categoria)
                .OrderBy(r => r.Nombre)
                .Select(r => new RepuestoListadoViewModel
                {
                    Id = r.Id,
                    Codigo = r.Codigo,
                    Nombre = r.Nombre,
                    CategoriaNombre = r.Categoria != null ? r.Categoria.Nombre : string.Empty,
                    PrecioCostoPromedio = r.PrecioCostoPromedio,
                    MargenGananciaPorcentaje = r.MargenGananciaPorcentaje,
                    DescuentoPorcentaje = r.DescuentoPorcentaje,
                    Activo = r.Activo,
                    StockMinimoGlobal = r.StockMinimoGlobal,

                    // 🔹 Suma de stock en todas las bodegas para este repuesto
                    StockTotal = _context.StocksRepuestosBodegas
                        .Where(s => s.RepuestoId == r.Id)
                        .Sum(s => (int?)s.Cantidad) ?? 0
                })
                .AsNoTracking()
                .ToListAsync();

            return View(repuestos);
        }

        // ===============================================================
        // DETALLES
        // ===============================================================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var repuesto = await _context.Repuestos
                .Include(r => r.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (repuesto == null)
                return NotFound();

            return View(repuesto);
        }

        // ===============================================================
        // CREAR (GET)
        // ===============================================================
        public IActionResult Create()
        {
            CargarCategoriasDropDown();
            return View(new Repuesto
            {
                Activo = true,
                PrecioCostoPromedio = 0,
                MargenGananciaPorcentaje = 0,
                StockMinimoGlobal = 0
            });
        }

        // ===============================================================
        // CREAR (POST)
        // ===============================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Repuesto repuesto)
        {
            if (!ModelState.IsValid)
            {
                CargarCategoriasDropDown(repuesto.CategoriaId);
                return View(repuesto);
            }

            try
            {
                _context.Repuestos.Add(repuesto);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Repuesto creado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var mensaje = ex.InnerException?.Message ?? ex.Message;
                ModelState.AddModelError(string.Empty, $"Error al guardar el repuesto: {mensaje}");

                CargarCategoriasDropDown(repuesto.CategoriaId);
                return View(repuesto);
            }
        }

        // ===============================================================
        // EDITAR (GET)
        // ===============================================================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var repuesto = await _context.Repuestos.FindAsync(id);
            if (repuesto == null)
                return NotFound();

            CargarCategoriasDropDown(repuesto.CategoriaId);
            return View(repuesto);
        }

        // ===============================================================
        // EDITAR (POST)
        // ===============================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Repuesto repuesto)
        {
            if (id != repuesto.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                CargarCategoriasDropDown(repuesto.CategoriaId);
                return View(repuesto);
            }

            try
            {
                _context.Update(repuesto);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Repuesto actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RepuestoExists(repuesto.Id))
                    return NotFound();
                else
                    throw;
            }
            catch (Exception ex)
            {
                var mensaje = ex.InnerException?.Message ?? ex.Message;
                ModelState.AddModelError(string.Empty, $"Error al actualizar el repuesto: {mensaje}");

                CargarCategoriasDropDown(repuesto.CategoriaId);
                return View(repuesto);
            }
        }

        // ===============================================================
        // ELIMINAR (GET)
        // ===============================================================
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var repuesto = await _context.Repuestos
                .Include(r => r.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (repuesto == null)
                return NotFound();

            return View(repuesto);
        }

        // ===============================================================
        // ELIMINAR (POST)
        // ===============================================================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var repuesto = await _context.Repuestos.FindAsync(id);
            if (repuesto != null)
            {
                _context.Repuestos.Remove(repuesto);
                await _context.SaveChangesAsync();
            }

            TempData["SuccessMessage"] = "Repuesto eliminado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        // ===============================================================
        // HELPERS
        // ===============================================================
        private void CargarCategoriasDropDown(int? seleccionado = null)
        {
            var categorias = _context.Categorias
                .OrderBy(c => c.Nombre)
                .ToList();

            ViewBag.CategoriaId = new SelectList(categorias, "Id", "Nombre", seleccionado);
        }

        private bool RepuestoExists(int id)
        {
            return _context.Repuestos.Any(e => e.Id == id);
        }
    }
}
