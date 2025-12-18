using CrudNativo.Data;
using CrudNativo.Models;
using Microsoft.AspNetCore.Mvc;

namespace CrudNativo.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Producto
        public IActionResult Index()
        {
            IEnumerable<Producto> listaProductos = _context.Producto;
            return View(listaProductos);
        }

        public IActionResult Create()
        {
            return View();
        }

        //create get
        public IActionResult Edit() {
            return View();
        }

        //Create post
        [HttpPost]
        public IActionResult Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Producto.Add(producto);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(producto);
        }

        //HttpGet Edit Get
        public IActionResult Edit(string? nombre)
        {
            if (nombre == null || nombre.Length == 0)
            {
                return NotFound();
            }
            var producto = _context.Producto.Find(nombre);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        //HttpPost Edit Post
        [HttpPost]
        public IActionResult Edit(Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Producto.Update(producto);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(producto);


        }
        //HttpGet Delete Get
        public IActionResult Delete(string? nombre)
        {
            if (nombre == null || nombre.Length == 0)
            {
                return NotFound();
            }
            var producto = _context.Producto.Find(nombre);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }
        //HttpPost Delete Post
        [HttpPost]
        public IActionResult Delete(Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Producto.Remove(producto);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(producto);


        }

    }

}

