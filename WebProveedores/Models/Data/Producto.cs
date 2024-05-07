using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProveedores.Models.Data
{
    [Index(nameof(IdProveedor), nameof(Codigo), IsUnique = true)]
    [Table("Producto")]
    public class Producto
    {
        [Key]
        public int IdProducto { get; set; }

        [ForeignKey("Proveedor")]
        public int IdProveedor { get; set; }
        public Proveedor? Proveedor { get; set; }

        [MaxLength(20, ErrorMessage = "No debe exceder los 20 caracteres")]
        [Column(TypeName = "varchar(20)")]
        public string Codigo { get; set; }

        [MaxLength(150, ErrorMessage = "No debe exceder los 150 caracteres")]
        [Column(TypeName = "varchar(150)")]
        public string Descripcion { get; set; }

        [MaxLength(3, ErrorMessage = "No debe exceder los 3 caracteres")]
        [Column(TypeName = "varchar(3)")]
        public string Unidad { get; set; }


        [Column(TypeName = "decimal(18, 2)")]
        public decimal Costo { get; set; }

    }
}
