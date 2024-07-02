using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v71 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdEmpresa",
                table: "CombateIncendioDecolagemPouso",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CombateIncendioDecolagemPouso_IdEmpresa",
                table: "CombateIncendioDecolagemPouso",
                column: "IdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_CombateIncendioDecolagemPouso_Empresa_IdEmpresa",
                table: "CombateIncendioDecolagemPouso",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CombateIncendioDecolagemPouso_Empresa_IdEmpresa",
                table: "CombateIncendioDecolagemPouso");

            migrationBuilder.DropIndex(
                name: "IX_CombateIncendioDecolagemPouso_IdEmpresa",
                table: "CombateIncendioDecolagemPouso");

            migrationBuilder.DropColumn(
                name: "IdEmpresa",
                table: "CombateIncendioDecolagemPouso");
        }
    }
}
