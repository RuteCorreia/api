using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v88 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdEmpresa",
                table: "Bula",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bula_IdEmpresa",
                table: "Bula",
                column: "IdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_Bula_Empresa_IdEmpresa",
                table: "Bula",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bula_Empresa_IdEmpresa",
                table: "Bula");

            migrationBuilder.DropIndex(
                name: "IX_Bula_IdEmpresa",
                table: "Bula");

            migrationBuilder.DropColumn(
                name: "IdEmpresa",
                table: "Bula");
        }
    }
}
