using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v46 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdEmpresa",
                table: "DadosResponsavel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdEmpresa",
                table: "ContratoPrestacaoServico",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DadosResponsavel_IdEmpresa",
                table: "DadosResponsavel",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_ContratoPrestacaoServico_IdEmpresa",
                table: "ContratoPrestacaoServico",
                column: "IdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_ContratoPrestacaoServico_Empresa_IdEmpresa",
                table: "ContratoPrestacaoServico",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_DadosResponsavel_Empresa_IdEmpresa",
                table: "DadosResponsavel",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContratoPrestacaoServico_Empresa_IdEmpresa",
                table: "ContratoPrestacaoServico");

            migrationBuilder.DropForeignKey(
                name: "FK_DadosResponsavel_Empresa_IdEmpresa",
                table: "DadosResponsavel");

            migrationBuilder.DropIndex(
                name: "IX_DadosResponsavel_IdEmpresa",
                table: "DadosResponsavel");

            migrationBuilder.DropIndex(
                name: "IX_ContratoPrestacaoServico_IdEmpresa",
                table: "ContratoPrestacaoServico");

            migrationBuilder.DropColumn(
                name: "IdEmpresa",
                table: "DadosResponsavel");

            migrationBuilder.DropColumn(
                name: "IdEmpresa",
                table: "ContratoPrestacaoServico");
        }
    }
}
