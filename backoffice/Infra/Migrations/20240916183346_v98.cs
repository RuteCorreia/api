using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v98 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdProduto",
                table: "Bula",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnidadeProduto",
                table: "Bula",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bula_IdProduto",
                table: "Bula",
                column: "IdProduto");

            migrationBuilder.AddForeignKey(
                name: "FK_Bula_Produto_IdProduto",
                table: "Bula",
                column: "IdProduto",
                principalTable: "Produto",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bula_Produto_IdProduto",
                table: "Bula");

            migrationBuilder.DropIndex(
                name: "IX_Bula_IdProduto",
                table: "Bula");

            migrationBuilder.DropColumn(
                name: "IdProduto",
                table: "Bula");

            migrationBuilder.DropColumn(
                name: "UnidadeProduto",
                table: "Bula");
        }
    }
}
