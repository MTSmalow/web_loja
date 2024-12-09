using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebLoja.Migrations
{
    public partial class SeedUsuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Autenticacao", "Email", "Endereco", "Nome" },
                values: new object[] { 1, "SKmH48EJHaCuj+528OWOzr9r17D/qwOxxzRplydB+Nc=", "cliente@cliente.com", "Rua Ribeirão Preto, 75 Mauá-SP", "Cliente" });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Autenticacao", "Email", "Endereco", "Nome" },
                values: new object[] { 2, "r2wwX8N1W1w7pocd+OMMPVwkEh3JkZ+1gW/momxDnq0=", "admin@admin", "Rua Ribeirão Preto, 75 Mauá-SP", "Administrador" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
