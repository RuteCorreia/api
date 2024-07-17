using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v83 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdEmpresa",
                table: "PlanilhaExcelExportadas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlanilhaExcelExportadas_IdEmpresa",
                table: "PlanilhaExcelExportadas",
                column: "IdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanilhaExcelExportadas_Empresa_IdEmpresa",
                table: "PlanilhaExcelExportadas",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanilhaExcelExportadas_Empresa_IdEmpresa",
                table: "PlanilhaExcelExportadas");

            migrationBuilder.DropIndex(
                name: "IX_PlanilhaExcelExportadas_IdEmpresa",
                table: "PlanilhaExcelExportadas");

            migrationBuilder.DropColumn(
                name: "IdEmpresa",
                table: "PlanilhaExcelExportadas");
        }
    }
}
