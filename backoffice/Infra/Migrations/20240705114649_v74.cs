using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v74 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdData",
                table: "RelatorioAplicacao",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Marcadores",
                table: "IdentificacaoAreaTratada",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdData",
                table: "CombateIncendio",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DataRelatorio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataRelatorio", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RelatorioAplicacao_IdData",
                table: "RelatorioAplicacao",
                column: "IdData");

            migrationBuilder.CreateIndex(
                name: "IX_CombateIncendio_IdData",
                table: "CombateIncendio",
                column: "IdData");

            migrationBuilder.AddForeignKey(
                name: "FK_CombateIncendio_DataRelatorio_IdData",
                table: "CombateIncendio",
                column: "IdData",
                principalTable: "DataRelatorio",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatorioAplicacao_DataRelatorio_IdData",
                table: "RelatorioAplicacao",
                column: "IdData",
                principalTable: "DataRelatorio",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CombateIncendio_DataRelatorio_IdData",
                table: "CombateIncendio");

            migrationBuilder.DropForeignKey(
                name: "FK_RelatorioAplicacao_DataRelatorio_IdData",
                table: "RelatorioAplicacao");

            migrationBuilder.DropTable(
                name: "DataRelatorio");

            migrationBuilder.DropIndex(
                name: "IX_RelatorioAplicacao_IdData",
                table: "RelatorioAplicacao");

            migrationBuilder.DropIndex(
                name: "IX_CombateIncendio_IdData",
                table: "CombateIncendio");

            migrationBuilder.DropColumn(
                name: "IdData",
                table: "RelatorioAplicacao");

            migrationBuilder.DropColumn(
                name: "Marcadores",
                table: "IdentificacaoAreaTratada");

            migrationBuilder.DropColumn(
                name: "IdData",
                table: "CombateIncendio");
        }
    }
}
