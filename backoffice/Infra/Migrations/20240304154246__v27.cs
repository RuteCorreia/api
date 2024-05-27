using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class _v27 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aeronave_Empresa_IdEmpresa",
                table: "Aeronave");

            migrationBuilder.DropIndex(
                name: "IX_Aeronave_IdEmpresa",
                table: "Aeronave");

            migrationBuilder.DropColumn(
                name: "IdEmpresa",
                table: "Aeronave");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdEmpresa",
                table: "Aeronave",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Aeronave_IdEmpresa",
                table: "Aeronave",
                column: "IdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_Aeronave_Empresa_IdEmpresa",
                table: "Aeronave",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa");
        }
    }
}
