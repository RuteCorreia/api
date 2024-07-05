using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v75 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdEmpresa",
                table: "DataRelatorio",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DataRelatorio_IdEmpresa",
                table: "DataRelatorio",
                column: "IdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_DataRelatorio_Empresa_IdEmpresa",
                table: "DataRelatorio",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataRelatorio_Empresa_IdEmpresa",
                table: "DataRelatorio");

            migrationBuilder.DropIndex(
                name: "IX_DataRelatorio_IdEmpresa",
                table: "DataRelatorio");

            migrationBuilder.DropColumn(
                name: "IdEmpresa",
                table: "DataRelatorio");
        }
    }
}
