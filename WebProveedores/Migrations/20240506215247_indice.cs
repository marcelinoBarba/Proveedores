using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProveedores.Migrations
{
    /// <inheritdoc />
    public partial class indice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Producto_IdProveedor",
                table: "Producto");

            migrationBuilder.CreateIndex(
                name: "IX_Proveedor_Codigo",
                table: "Proveedor",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Producto_IdProveedor_Codigo",
                table: "Producto",
                columns: new[] { "IdProveedor", "Codigo" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Proveedor_Codigo",
                table: "Proveedor");

            migrationBuilder.DropIndex(
                name: "IX_Producto_IdProveedor_Codigo",
                table: "Producto");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_IdProveedor",
                table: "Producto",
                column: "IdProveedor");
        }
    }
}
