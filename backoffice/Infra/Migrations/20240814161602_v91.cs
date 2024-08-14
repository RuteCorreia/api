using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v91 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdData",
                table: "ControleDeFrota",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdEmpresa",
                table: "ControleDeFrota",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ControleDeFrota_IdData",
                table: "ControleDeFrota",
                column: "IdData");

            migrationBuilder.CreateIndex(
                name: "IX_ControleDeFrota_IdEmpresa",
                table: "ControleDeFrota",
                column: "IdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_ControleDeFrota_DataRelatorio_IdData",
                table: "ControleDeFrota",
                column: "IdData",
                principalTable: "DataRelatorio",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ControleDeFrota_Empresa_IdEmpresa",
                table: "ControleDeFrota",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ControleDeFrota_DataRelatorio_IdData",
                table: "ControleDeFrota");

            migrationBuilder.DropForeignKey(
                name: "FK_ControleDeFrota_Empresa_IdEmpresa",
                table: "ControleDeFrota");

            migrationBuilder.DropIndex(
                name: "IX_ControleDeFrota_IdData",
                table: "ControleDeFrota");

            migrationBuilder.DropIndex(
                name: "IX_ControleDeFrota_IdEmpresa",
                table: "ControleDeFrota");

            migrationBuilder.DropColumn(
                name: "IdData",
                table: "ControleDeFrota");

            migrationBuilder.DropColumn(
                name: "IdEmpresa",
                table: "ControleDeFrota");
        }
    }
}
