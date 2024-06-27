using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v59 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Piloto",
                table: "RelatorioAplicacao",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Executor",
                table: "RelatorioAplicacao",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCriacao",
                table: "RelatorioAplicacao",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "AuxiliarPistaId",
                table: "RelatorioAplicacao",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "RelatorioAplicacao",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CulturaId",
                table: "RelatorioAplicacao",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "RelatorioAplicacao",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExecutorId",
                table: "RelatorioAplicacao",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDrone",
                table: "RelatorioAplicacao",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PilotoId",
                table: "RelatorioAplicacao",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AuxiliarPista",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Documento = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuxiliarPista", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RelatorioAplicacao_AuxiliarPistaId",
                table: "RelatorioAplicacao",
                column: "AuxiliarPistaId");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatorioAplicacao_AuxiliarPista_AuxiliarPistaId",
                table: "RelatorioAplicacao",
                column: "AuxiliarPistaId",
                principalTable: "AuxiliarPista",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RelatorioAplicacao_AuxiliarPista_AuxiliarPistaId",
                table: "RelatorioAplicacao");

            migrationBuilder.DropTable(
                name: "AuxiliarPista");

            migrationBuilder.DropIndex(
                name: "IX_RelatorioAplicacao_AuxiliarPistaId",
                table: "RelatorioAplicacao");

            migrationBuilder.DropColumn(
                name: "AuxiliarPistaId",
                table: "RelatorioAplicacao");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "RelatorioAplicacao");

            migrationBuilder.DropColumn(
                name: "CulturaId",
                table: "RelatorioAplicacao");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "RelatorioAplicacao");

            migrationBuilder.DropColumn(
                name: "ExecutorId",
                table: "RelatorioAplicacao");

            migrationBuilder.DropColumn(
                name: "IsDrone",
                table: "RelatorioAplicacao");

            migrationBuilder.DropColumn(
                name: "PilotoId",
                table: "RelatorioAplicacao");

            migrationBuilder.AlterColumn<string>(
                name: "Piloto",
                table: "RelatorioAplicacao",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Executor",
                table: "RelatorioAplicacao",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCriacao",
                table: "RelatorioAplicacao",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
