using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v51 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HorasInspecao",
                table: "ManutencaoAeronave");

            migrationBuilder.DropColumn(
                name: "HorasRevisao",
                table: "ManutencaoAeronave");

            migrationBuilder.RenameColumn(
                name: "HorimetroInicial",
                table: "ManutencaoAeronave",
                newName: "Horimetro");

            migrationBuilder.AddColumn<byte[]>(
                name: "FichaInspecao",
                table: "ManutencaoAeronave",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ManualAeronave",
                table: "ManutencaoAeronave",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "MapaComponentes",
                table: "ManutencaoAeronave",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FichaInspecao",
                table: "ManutencaoAeronave");

            migrationBuilder.DropColumn(
                name: "ManualAeronave",
                table: "ManutencaoAeronave");

            migrationBuilder.DropColumn(
                name: "MapaComponentes",
                table: "ManutencaoAeronave");

            migrationBuilder.RenameColumn(
                name: "Horimetro",
                table: "ManutencaoAeronave",
                newName: "HorimetroInicial");

            migrationBuilder.AddColumn<string>(
                name: "HorasInspecao",
                table: "ManutencaoAeronave",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HorasRevisao",
                table: "ManutencaoAeronave",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
