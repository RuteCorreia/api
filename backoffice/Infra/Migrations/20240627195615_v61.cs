using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v61 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RelatorioAplicacao_AplicacaoRelatorioId",
                table: "RelatorioAplicacao",
                column: "AplicacaoRelatorioId");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatorioAplicacao_AplicacaoRelatorio_AplicacaoRelatorioId",
                table: "RelatorioAplicacao",
                column: "AplicacaoRelatorioId",
                principalTable: "AplicacaoRelatorio",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RelatorioAplicacao_AplicacaoRelatorio_AplicacaoRelatorioId",
                table: "RelatorioAplicacao");

            migrationBuilder.DropIndex(
                name: "IX_RelatorioAplicacao_AplicacaoRelatorioId",
                table: "RelatorioAplicacao");
        }
    }
}
