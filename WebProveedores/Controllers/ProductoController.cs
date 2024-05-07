using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProveedores.Models.Data;

namespace WebProveedores.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductoController> _logger;

        public ProductoController(ApplicationDbContext context, ILogger<ProductoController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index(int? id)
        {

            if (id == null)
            {
                var productosTodos = _context.Productos.Include(p => p.Proveedor);
                return View(productosTodos.ToList());
            }

            var proveedor = _context.Proveedores.Where(w => w.IdProveedor == id).FirstOrDefault();
            ViewData["ProveedorCodigo"] = proveedor.Codigo;
            ViewData["ProveedorId"] = proveedor.IdProveedor;

            var productos = _context.Productos.Where(w => w.IdProveedor == id);
            return View(productos.ToList());

        }


        public IActionResult Create(int? id)
        {
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "Codigo", id);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producto producto)
        {

            if (ModelState.IsValid)
            {
                var codigoExistente = _context.Productos.Where(w => w.Codigo == producto.Codigo && w.IdProveedor == producto.IdProveedor).FirstOrDefault();
                if (codigoExistente == null)
                {
                    _logger.LogWarning("[ProductoController] [Create] - Codigo ya existe");
                    ModelState.AddModelError("Codigo", "Ya existe un producto con este codigo.");
                    return View(producto);

                }

                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();

                foreach (var item in errors)
                {
                    _logger.LogCritical("[ProductoController] [Create] - error al intentar crear un producto {" + item.FirstOrDefault().ErrorMessage + "}");

                }

            }

            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "Codigo", producto.IdProveedor);
            return View(producto);
        }


    }
}
