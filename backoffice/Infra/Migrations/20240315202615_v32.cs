using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v32 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FrotaRelatoriosAplicacaoIncendio",
                table: "Empresa",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Manutencao",
                table: "Empresa",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "QtdAeronaves",
                table: "Empresa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QtdDrones",
                table: "Empresa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QtdVeiculos",
                table: "Empresa",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FrotaRelatoriosAplicacaoIncendio",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "Manutencao",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "QtdAeronaves",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "QtdDrones",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "QtdVeiculos",
                table: "Empresa");
        }
    }
}
