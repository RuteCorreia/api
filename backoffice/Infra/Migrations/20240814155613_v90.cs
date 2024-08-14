using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v90 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "QtdeCombustivel",
                table: "ControleDeFrota",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HorimetroInicial",
                table: "ControleDeFrota",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HorimetroFinal",
                table: "ControleDeFrota",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CombustivelFinal",
                table: "ControleDeFrota",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CombustivelInicial",
                table: "ControleDeFrota",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "ControleDeFrota",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "ControleDeFrota",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Extensao",
                table: "ControleDeFrota",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IdExecutor",
                table: "ControleDeFrota",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdVeiculo",
                table: "ControleDeFrota",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeAeronave",
                table: "ControleDeFrota",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeExecutor",
                table: "ControleDeFrota",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomePiloto",
                table: "ControleDeFrota",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeVeiculo",
                table: "ControleDeFrota",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ControleDeFrota_IdExecutor",
                table: "ControleDeFrota",
                column: "IdExecutor");

            migrationBuilder.CreateIndex(
                name: "IX_ControleDeFrota_IdVeiculo",
                table: "ControleDeFrota",
                column: "IdVeiculo");

            migrationBuilder.AddForeignKey(
                name: "FK_ControleDeFrota_Usuario_IdExecutor",
                table: "ControleDeFrota",
                column: "IdExecutor",
                principalTable: "Usuario",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ControleDeFrota_Veiculo_IdVeiculo",
                table: "ControleDeFrota",
                column: "IdVeiculo",
                principalTable: "Veiculo",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ControleDeFrota_Usuario_IdExecutor",
                table: "ControleDeFrota");

            migrationBuilder.DropForeignKey(
                name: "FK_ControleDeFrota_Veiculo_IdVeiculo",
                table: "ControleDeFrota");

            migrationBuilder.DropIndex(
                name: "IX_ControleDeFrota_IdExecutor",
                table: "ControleDeFrota");

            migrationBuilder.DropIndex(
                name: "IX_ControleDeFrota_IdVeiculo",
                table: "ControleDeFrota");

            migrationBuilder.DropColumn(
                name: "CombustivelFinal",
                table: "ControleDeFrota");

            migrationBuilder.DropColumn(
                name: "CombustivelInicial",
                table: "ControleDeFrota");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "ControleDeFrota");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "ControleDeFrota");

            migrationBuilder.DropColumn(
                name: "Extensao",
                table: "ControleDeFrota");

            migrationBuilder.DropColumn(
                name: "IdExecutor",
                table: "ControleDeFrota");

            migrationBuilder.DropColumn(
                name: "IdVeiculo",
                table: "ControleDeFrota");

            migrationBuilder.DropColumn(
                name: "NomeAeronave",
                table: "ControleDeFrota");

            migrationBuilder.DropColumn(
                name: "NomeExecutor",
                table: "ControleDeFrota");

            migrationBuilder.DropColumn(
                name: "NomePiloto",
                table: "ControleDeFrota");

            migrationBuilder.DropColumn(
                name: "NomeVeiculo",
                table: "ControleDeFrota");

            migrationBuilder.AlterColumn<int>(
                name: "QtdeCombustivel",
                table: "ControleDeFrota",
                type: "int",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HorimetroInicial",
                table: "ControleDeFrota",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HorimetroFinal",
                table: "ControleDeFrota",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
