using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v65 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdEmpresa",
                table: "RelatorioAplicacao",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdEmpresa",
                table: "IdentificacaoAreaTratada",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdEmpresa",
                table: "Contratante",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdEmpresa",
                table: "AuxiliarPista",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdEmpresa",
                table: "AplicacaoRelatorioItem",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdEmpresa",
                table: "AplicacaoRelatorio",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdEmpresa",
                table: "AplicacaoRecomendacoesTecnicas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RelatorioAplicacao_IdEmpresa",
                table: "RelatorioAplicacao",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_IdentificacaoAreaTratada_IdEmpresa",
                table: "IdentificacaoAreaTratada",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_Contratante_IdEmpresa",
                table: "Contratante",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_AuxiliarPista_IdEmpresa",
                table: "AuxiliarPista",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_AplicacaoRelatorioItem_IdEmpresa",
                table: "AplicacaoRelatorioItem",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_AplicacaoRelatorio_IdEmpresa",
                table: "AplicacaoRelatorio",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_AplicacaoRecomendacoesTecnicas_IdEmpresa",
                table: "AplicacaoRecomendacoesTecnicas",
                column: "IdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_AplicacaoRecomendacoesTecnicas_Empresa_IdEmpresa",
                table: "AplicacaoRecomendacoesTecnicas",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_AplicacaoRelatorio_Empresa_IdEmpresa",
                table: "AplicacaoRelatorio",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_AplicacaoRelatorioItem_Empresa_IdEmpresa",
                table: "AplicacaoRelatorioItem",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_AuxiliarPista_Empresa_IdEmpresa",
                table: "AuxiliarPista",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_Contratante_Empresa_IdEmpresa",
                table: "Contratante",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_IdentificacaoAreaTratada_Empresa_IdEmpresa",
                table: "IdentificacaoAreaTratada",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatorioAplicacao_Empresa_IdEmpresa",
                table: "RelatorioAplicacao",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AplicacaoRecomendacoesTecnicas_Empresa_IdEmpresa",
                table: "AplicacaoRecomendacoesTecnicas");

            migrationBuilder.DropForeignKey(
                name: "FK_AplicacaoRelatorio_Empresa_IdEmpresa",
                table: "AplicacaoRelatorio");

            migrationBuilder.DropForeignKey(
                name: "FK_AplicacaoRelatorioItem_Empresa_IdEmpresa",
                table: "AplicacaoRelatorioItem");

            migrationBuilder.DropForeignKey(
                name: "FK_AuxiliarPista_Empresa_IdEmpresa",
                table: "AuxiliarPista");

            migrationBuilder.DropForeignKey(
                name: "FK_Contratante_Empresa_IdEmpresa",
                table: "Contratante");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentificacaoAreaTratada_Empresa_IdEmpresa",
                table: "IdentificacaoAreaTratada");

            migrationBuilder.DropForeignKey(
                name: "FK_RelatorioAplicacao_Empresa_IdEmpresa",
                table: "RelatorioAplicacao");

            migrationBuilder.DropIndex(
                name: "IX_RelatorioAplicacao_IdEmpresa",
                table: "RelatorioAplicacao");

            migrationBuilder.DropIndex(
                name: "IX_IdentificacaoAreaTratada_IdEmpresa",
                table: "IdentificacaoAreaTratada");

            migrationBuilder.DropIndex(
                name: "IX_Contratante_IdEmpresa",
                table: "Contratante");

            migrationBuilder.DropIndex(
                name: "IX_AuxiliarPista_IdEmpresa",
                table: "AuxiliarPista");

            migrationBuilder.DropIndex(
                name: "IX_AplicacaoRelatorioItem_IdEmpresa",
                table: "AplicacaoRelatorioItem");

            migrationBuilder.DropIndex(
                name: "IX_AplicacaoRelatorio_IdEmpresa",
                table: "AplicacaoRelatorio");

            migrationBuilder.DropIndex(
                name: "IX_AplicacaoRecomendacoesTecnicas_IdEmpresa",
                table: "AplicacaoRecomendacoesTecnicas");

            migrationBuilder.DropColumn(
                name: "IdEmpresa",
                table: "RelatorioAplicacao");

            migrationBuilder.DropColumn(
                name: "IdEmpresa",
                table: "IdentificacaoAreaTratada");

            migrationBuilder.DropColumn(
                name: "IdEmpresa",
                table: "Contratante");

            migrationBuilder.DropColumn(
                name: "IdEmpresa",
                table: "AuxiliarPista");

            migrationBuilder.DropColumn(
                name: "IdEmpresa",
                table: "AplicacaoRelatorioItem");

            migrationBuilder.DropColumn(
                name: "IdEmpresa",
                table: "AplicacaoRelatorio");

            migrationBuilder.DropColumn(
                name: "IdEmpresa",
                table: "AplicacaoRecomendacoesTecnicas");
        }
    }
}
