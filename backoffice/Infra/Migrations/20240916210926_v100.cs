using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v100 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TipoDeFormulacao",
                table: "Produto",
                newName: "TipoFormulacao");

            migrationBuilder.AddColumn<int>(
                name: "IdTipoDeFormulacao",
                table: "Produto",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdTipoDeServico",
                table: "Produto",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produto_IdTipoDeFormulacao",
                table: "Produto",
                column: "IdTipoDeFormulacao");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_IdTipoDeServico",
                table: "Produto",
                column: "IdTipoDeServico");

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_TipoDeFormulacao_IdTipoDeFormulacao",
                table: "Produto",
                column: "IdTipoDeFormulacao",
                principalTable: "TipoDeFormulacao",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_TipoDeServico_IdTipoDeServico",
                table: "Produto",
                column: "IdTipoDeServico",
                principalTable: "TipoDeServico",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produto_TipoDeFormulacao_IdTipoDeFormulacao",
                table: "Produto");

            migrationBuilder.DropForeignKey(
                name: "FK_Produto_TipoDeServico_IdTipoDeServico",
                table: "Produto");

            migrationBuilder.DropIndex(
                name: "IX_Produto_IdTipoDeFormulacao",
                table: "Produto");

            migrationBuilder.DropIndex(
                name: "IX_Produto_IdTipoDeServico",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "IdTipoDeFormulacao",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "IdTipoDeServico",
                table: "Produto");

            migrationBuilder.RenameColumn(
                name: "TipoFormulacao",
                table: "Produto",
                newName: "TipoDeFormulacao");
        }
    }
}
