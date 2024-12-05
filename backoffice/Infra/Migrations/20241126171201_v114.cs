using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v114 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdEmpresa",
                table: "TipoDeFormulacao",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TipoDeFormulacao_IdEmpresa",
                table: "TipoDeFormulacao",
                column: "IdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_TipoDeFormulacao_Empresa_IdEmpresa",
                table: "TipoDeFormulacao",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TipoDeFormulacao_Empresa_IdEmpresa",
                table: "TipoDeFormulacao");

            migrationBuilder.DropIndex(
                name: "IX_TipoDeFormulacao_IdEmpresa",
                table: "TipoDeFormulacao");

            migrationBuilder.DropColumn(
                name: "IdEmpresa",
                table: "TipoDeFormulacao");
        }
    }
}
