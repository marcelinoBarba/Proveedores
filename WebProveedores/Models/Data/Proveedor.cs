using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebProveedores.Models.Data
{
    [Index(nameof(Codigo), IsUnique = true) ]
    [Table("Proveedor")]
    public class Proveedor
    {
        [Key]
        public int IdProveedor { get; set; }

        
        [MaxLength(20, ErrorMessage = "No debe exceder los 20 caracteres")]
        [Column(TypeName = "varchar(20)")]
        public string Codigo { get; set; }

        [MaxLength(150, ErrorMessage = "No debe exceder los 150 caracteres")]
        [Column(TypeName = "varchar(150)")]
        public string RazonSocial { get; set; }

        [Required(ErrorMessage = "El RFC es obligatorio")]
        [RegularExpression(@"^([A-ZÑ\x26]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1])([A-Z]|[0-9]){2}([A]|[0-9]){1})?$", ErrorMessage = "El RFC no es válido")]
        [MaxLength(13, ErrorMessage = "No debe exceder los 13 caracteres")]
        [Column(TypeName = "varchar(13)")]
        public string RFC { get; set; }
    }
}
