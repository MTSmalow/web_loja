using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebLoja.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "Categoria", "Descricao", "Foto", "Nome", "Preco" },
                values: new object[] { 1, "", "", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQY6giYFcAsV5wX9z0E5sudLWKwATbkO3IoUQ&s", "SNES", 15000m });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "Categoria", "Descricao", "Foto", "Nome", "Preco" },
                values: new object[] { 2, "", "", "https://lazamodz.com/cdn/shop/files/PS1Classic_800x.png?v=1698695300", "PS5 Controller", 1500m });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "Categoria", "Descricao", "Foto", "Nome", "Preco" },
                values: new object[] { 3, "", "", "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/NEC-TurboExpress-Upright-FL.jpg/220px-NEC-TurboExpress-Upright-FL.jpg", "Turbo Express", 15000m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
