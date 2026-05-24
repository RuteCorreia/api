using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddIdRefToTipoDeFormulacaoAndAlvoBiologico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdRef",
                table: "TipoDeFormulacao",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdRef",
                table: "AlvoBiologico",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TipoDeFormulacao_IdEmpresa_IdRef",
                table: "TipoDeFormulacao",
                columns: new[] { "IdEmpresa", "IdRef" });

            migrationBuilder.CreateIndex(
                name: "IX_AlvoBiologico_IdEmpresa_IdRef",
                table: "AlvoBiologico",
                columns: new[] { "IdEmpresa", "IdRef" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TipoDeFormulacao_IdEmpresa_IdRef",
                table: "TipoDeFormulacao");

            migrationBuilder.DropIndex(
                name: "IX_AlvoBiologico_IdEmpresa_IdRef",
                table: "AlvoBiologico");

            migrationBuilder.DropColumn(
                name: "IdRef",
                table: "TipoDeFormulacao");

            migrationBuilder.DropColumn(
                name: "IdRef",
                table: "AlvoBiologico");
        }
    }
}
