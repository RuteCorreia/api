using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v39 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdEmpresa",
                table: "ManutencaoAeronave",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdEmpresa",
                table: "Componente",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdEmpresa",
                table: "Aeronave",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ManutencaoAeronave_IdEmpresa",
                table: "ManutencaoAeronave",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_Componente_IdEmpresa",
                table: "Componente",
                column: "IdEmpresa");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Componente_Empresa_IdEmpresa",
                table: "Componente",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_ManutencaoAeronave_Empresa_IdEmpresa",
                table: "ManutencaoAeronave",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aeronave_Empresa_IdEmpresa",
                table: "Aeronave");

            migrationBuilder.DropForeignKey(
                name: "FK_Componente_Empresa_IdEmpresa",
                table: "Componente");

            migrationBuilder.DropForeignKey(
                name: "FK_ManutencaoAeronave_Empresa_IdEmpresa",
                table: "ManutencaoAeronave");

            migrationBuilder.DropIndex(
                name: "IX_ManutencaoAeronave_IdEmpresa",
                table: "ManutencaoAeronave");

            migrationBuilder.DropIndex(
                name: "IX_Componente_IdEmpresa",
                table: "Componente");

            migrationBuilder.DropIndex(
                name: "IX_Aeronave_IdEmpresa",
                table: "Aeronave");

            migrationBuilder.DropColumn(
                name: "IdEmpresa",
                table: "ManutencaoAeronave");

            migrationBuilder.DropColumn(
                name: "IdEmpresa",
                table: "Componente");

            migrationBuilder.DropColumn(
                name: "IdEmpresa",
                table: "Aeronave");
        }
    }
}
