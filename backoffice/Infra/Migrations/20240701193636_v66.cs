using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v66 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CapacidadeCargaAeronave",
                table: "CombateIncendio",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "CombateIncendio",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cliente",
                table: "CombateIncendio",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContratoPrestacaoServicoId",
                table: "CombateIncendio",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Observacao",
                table: "CombateIncendio",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Piloto",
                table: "CombateIncendio",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Uf",
                table: "CombateIncendio",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CombateIncendio_ContratoPrestacaoServicoId",
                table: "CombateIncendio",
                column: "ContratoPrestacaoServicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CombateIncendio_ContratoPrestacaoServico_ContratoPrestacaoServicoId",
                table: "CombateIncendio",
                column: "ContratoPrestacaoServicoId",
                principalTable: "ContratoPrestacaoServico",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CombateIncendio_ContratoPrestacaoServico_ContratoPrestacaoServicoId",
                table: "CombateIncendio");

            migrationBuilder.DropIndex(
                name: "IX_CombateIncendio_ContratoPrestacaoServicoId",
                table: "CombateIncendio");

            migrationBuilder.DropColumn(
                name: "CapacidadeCargaAeronave",
                table: "CombateIncendio");

            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "CombateIncendio");

            migrationBuilder.DropColumn(
                name: "Cliente",
                table: "CombateIncendio");

            migrationBuilder.DropColumn(
                name: "ContratoPrestacaoServicoId",
                table: "CombateIncendio");

            migrationBuilder.DropColumn(
                name: "Observacao",
                table: "CombateIncendio");

            migrationBuilder.DropColumn(
                name: "Piloto",
                table: "CombateIncendio");

            migrationBuilder.DropColumn(
                name: "Uf",
                table: "CombateIncendio");
        }
    }
}
