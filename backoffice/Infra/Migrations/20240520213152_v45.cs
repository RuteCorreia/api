using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v45 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdEmpresa",
                table: "CaracteristicasProdutoAplicado",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CaracteristicasProdutoAplicado_IdEmpresa",
                table: "CaracteristicasProdutoAplicado",
                column: "IdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_CaracteristicasProdutoAplicado_Empresa_IdEmpresa",
                table: "CaracteristicasProdutoAplicado",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CaracteristicasProdutoAplicado_Empresa_IdEmpresa",
                table: "CaracteristicasProdutoAplicado");

            migrationBuilder.DropIndex(
                name: "IX_CaracteristicasProdutoAplicado_IdEmpresa",
                table: "CaracteristicasProdutoAplicado");

            migrationBuilder.DropColumn(
                name: "IdEmpresa",
                table: "CaracteristicasProdutoAplicado");
        }
    }
}
