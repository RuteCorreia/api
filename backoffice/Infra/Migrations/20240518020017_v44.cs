using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v44 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desativada",
                table: "Empresa");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Empresa",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Empresa");

            migrationBuilder.AddColumn<bool>(
                name: "Desativada",
                table: "Empresa",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
