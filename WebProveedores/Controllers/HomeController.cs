using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebProveedores.Models;
using WebProveedores.Models.Data;

namespace WebProveedores.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _context = context; 
            _logger = logger;
        }

        public IActionResult Index()
        {
            var proveedoresCount = _context.Proveedores.Count();

            ViewData["ProveedoresCount"] = proveedoresCount;
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
