using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v70 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdEmpresa",
                table: "CombateIncendioPista",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CombateIncendioPista_IdEmpresa",
                table: "CombateIncendioPista",
                column: "IdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_CombateIncendioPista_Empresa_IdEmpresa",
                table: "CombateIncendioPista",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CombateIncendioPista_Empresa_IdEmpresa",
                table: "CombateIncendioPista");

            migrationBuilder.DropIndex(
                name: "IX_CombateIncendioPista_IdEmpresa",
                table: "CombateIncendioPista");

            migrationBuilder.DropColumn(
                name: "IdEmpresa",
                table: "CombateIncendioPista");
        }
    }
}
