using Microsoft.EntityFrameworkCore;

namespace WebProveedores.Models.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Producto> Productos { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
    }
}
