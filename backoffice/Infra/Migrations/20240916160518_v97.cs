using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v97 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdEmpresa",
                table: "Produto",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdEmpresa",
                table: "AlvoBiologico",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produto_IdEmpresa",
                table: "Produto",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_AlvoBiologico_IdEmpresa",
                table: "AlvoBiologico",
                column: "IdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_AlvoBiologico_Empresa_IdEmpresa",
                table: "AlvoBiologico",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_Empresa_IdEmpresa",
                table: "Produto",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlvoBiologico_Empresa_IdEmpresa",
                table: "AlvoBiologico");

            migrationBuilder.DropForeignKey(
                name: "FK_Produto_Empresa_IdEmpresa",
                table: "Produto");

            migrationBuilder.DropIndex(
                name: "IX_Produto_IdEmpresa",
                table: "Produto");

            migrationBuilder.DropIndex(
                name: "IX_AlvoBiologico_IdEmpresa",
                table: "AlvoBiologico");

            migrationBuilder.DropColumn(
                name: "IdEmpresa",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "IdEmpresa",
                table: "AlvoBiologico");
        }
    }
}
