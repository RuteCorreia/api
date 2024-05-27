using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CapacidadeDeCarga",
                table: "Aeronave");

            migrationBuilder.RenameColumn(
                name: "Horimetro",
                table: "Aeronave",
                newName: "SerialNumber");

            migrationBuilder.RenameColumn(
                name: "Combustivel",
                table: "Aeronave",
                newName: "Modelo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SerialNumber",
                table: "Aeronave",
                newName: "Horimetro");

            migrationBuilder.RenameColumn(
                name: "Modelo",
                table: "Aeronave",
                newName: "Combustivel");

            migrationBuilder.AddColumn<int>(
                name: "CapacidadeDeCarga",
                table: "Aeronave",
                type: "int",
                nullable: true);
        }
    }
}
