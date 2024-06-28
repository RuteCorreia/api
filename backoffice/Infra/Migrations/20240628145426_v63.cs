using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v63 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusEnvio",
                table: "RelatorioAplicacao",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContratanteRef",
                table: "Contratante",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsReceituarioImage",
                table: "CaracteristicasProdutoAplicado",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UrDoAR",
                table: "AplicacaoRecomendacoesTecnicas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusEnvio",
                table: "RelatorioAplicacao");

            migrationBuilder.DropColumn(
                name: "ContratanteRef",
                table: "Contratante");

            migrationBuilder.DropColumn(
                name: "IsReceituarioImage",
                table: "CaracteristicasProdutoAplicado");

            migrationBuilder.AlterColumn<int>(
                name: "UrDoAR",
                table: "AplicacaoRecomendacoesTecnicas",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
