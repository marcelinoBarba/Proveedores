using Microsoft.AspNetCore.Mvc;
using WebProveedores.Models.Data;

namespace WebProveedores.Controllers
{
    public class ProveedorController : Controller
    {
        private ApplicationDbContext _context;
        private readonly ILogger<ProveedorController> _logger;

        public ProveedorController(ApplicationDbContext context, ILogger<ProveedorController> logger)
        {
            _context = context;
            _logger = logger;
        }


        public IActionResult Index()
        {
            List<Proveedor> listProveedores = _context.Proveedores.ToList();
            return View(listProveedores);
        }

        public IActionResult Create()
        {
            return View(new Proveedor());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Proveedor proveedor)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();

                foreach (var item in errors)
                {
                    _logger.LogCritical("[ProveedorController] [Create] - error al intentar crear un proveedor {" + item.FirstOrDefault().ErrorMessage + "}");

                }

                return View(proveedor);
            }

            var codigoExistente = _context.Proveedores.Where(w => w.Codigo == proveedor.Codigo).FirstOrDefault();
            if (codigoExistente != null)
            {
                ModelState.AddModelError("Codigo", "Ya existe un proveedor con este codigo.");
                return View(proveedor);

            }

            _context.Proveedores.Add(proveedor);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var proveedor = _context.Proveedores.Where(w => w.IdProveedor == id).FirstOrDefault();
            if (proveedor == null)
            {
                _logger.LogWarning("[ProveedorController] [Edit] - No encontro ID para editar");
                return NotFound();
            }

            return View(proveedor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Proveedor proveedor)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();

                foreach (var item in errors)
                {
                    _logger.LogCritical("[ProveedorController] [Edit] - error al intentar crear un proveedor {" + item.FirstOrDefault().ErrorMessage + "}");

                }
                return View(proveedor);
            }

            _context.Proveedores.Update(proveedor);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            var proveedor = _context.Proveedores.FirstOrDefault(w => w.IdProveedor == id);
            if (proveedor == null)
            {
                _logger.LogWarning("[ProveedorController] [Delete] - No encontro ID para eliminar");
                return NotFound();
            }

            bool tieneProductos = _context.Productos.Any(p => p.IdProveedor == id);

            if (tieneProductos)
            {
                return RedirectToAction("ConfirmDelete", new { id = proveedor.IdProveedor });
            }
            else
            {
                _context.Proveedores.Remove(proveedor);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }


        public IActionResult ConfirmDelete(int id)
        {
            var proveedor = _context.Proveedores.FirstOrDefault(w => w.IdProveedor == id);
            if (proveedor == null)
            {
                _logger.LogWarning("[ProveedorController] [ConfirmDelete] - No encontro ID para eliminar");
                return NotFound();
            }

            return View(proveedor);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDeleteConfirmed(int id)
        {
            var proveedor = _context.Proveedores.FirstOrDefault(w => w.IdProveedor == id);
            if (proveedor != null)
            {
                _logger.LogWarning("[ProveedorController] [ConfirmDeleteConfirmed] - No encontro ID para eliminar");
                return NotFound();
            }

            _context.Proveedores.Remove(proveedor);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
