using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v60 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "RelatorioAplicacao");

            migrationBuilder.RenameColumn(
                name: "RelatorioAplicacaoId",
                table: "RelatorioAplicacao",
                newName: "AplicacaoRelatorioId");

            migrationBuilder.AddColumn<string>(
                name: "GravacaoArea",
                table: "IdentificacaoAreaTratada",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAplicacao",
                table: "AplicacaoRelatorioItem",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Alteracoes_Observacoes",
                table: "AplicacaoRelatorio",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Cultura",
                table: "AplicacaoRelatorio",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Densidade",
                table: "AplicacaoRelatorio",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "AplicacaoRelatorio",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocalizacaoPistaCodigoICAO",
                table: "AplicacaoRelatorio",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "AplicacaoRelatorio",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProdutoAplicado",
                table: "AplicacaoRelatorio",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RelatorioDGPS",
                table: "AplicacaoRelatorio",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnidadeVolumeAplicacao",
                table: "AplicacaoRelatorio",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArquivoDrone",
                table: "AplicacaoRecomendacoesTecnicas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeAeronave",
                table: "AplicacaoRecomendacoesTecnicas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeEquipamento",
                table: "AplicacaoRecomendacoesTecnicas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoDeProduto",
                table: "AplicacaoRecomendacoesTecnicas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnidadeVolumeAplicacao",
                table: "AplicacaoRecomendacoesTecnicas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Veinculante",
                table: "AplicacaoRecomendacoesTecnicas",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GravacaoArea",
                table: "IdentificacaoAreaTratada");

            migrationBuilder.DropColumn(
                name: "DataAplicacao",
                table: "AplicacaoRelatorioItem");

            migrationBuilder.DropColumn(
                name: "Cultura",
                table: "AplicacaoRelatorio");

            migrationBuilder.DropColumn(
                name: "Densidade",
                table: "AplicacaoRelatorio");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "AplicacaoRelatorio");

            migrationBuilder.DropColumn(
                name: "LocalizacaoPistaCodigoICAO",
                table: "AplicacaoRelatorio");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "AplicacaoRelatorio");

            migrationBuilder.DropColumn(
                name: "ProdutoAplicado",
                table: "AplicacaoRelatorio");

            migrationBuilder.DropColumn(
                name: "RelatorioDGPS",
                table: "AplicacaoRelatorio");

            migrationBuilder.DropColumn(
                name: "UnidadeVolumeAplicacao",
                table: "AplicacaoRelatorio");

            migrationBuilder.DropColumn(
                name: "ArquivoDrone",
                table: "AplicacaoRecomendacoesTecnicas");

            migrationBuilder.DropColumn(
                name: "NomeAeronave",
                table: "AplicacaoRecomendacoesTecnicas");

            migrationBuilder.DropColumn(
                name: "NomeEquipamento",
                table: "AplicacaoRecomendacoesTecnicas");

            migrationBuilder.DropColumn(
                name: "TipoDeProduto",
                table: "AplicacaoRecomendacoesTecnicas");

            migrationBuilder.DropColumn(
                name: "UnidadeVolumeAplicacao",
                table: "AplicacaoRecomendacoesTecnicas");

            migrationBuilder.DropColumn(
                name: "Veinculante",
                table: "AplicacaoRecomendacoesTecnicas");

            migrationBuilder.RenameColumn(
                name: "AplicacaoRelatorioId",
                table: "RelatorioAplicacao",
                newName: "RelatorioAplicacaoId");

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "RelatorioAplicacao",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Alteracoes_Observacoes",
                table: "AplicacaoRelatorio",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
