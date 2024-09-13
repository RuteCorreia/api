using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v94 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "PlanilhaExcelExportadas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "PlanilhaExcelExportadas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdEmpresa",
                table: "Pista",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cep",
                table: "Cliente",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pista_IdEmpresa",
                table: "Pista",
                column: "IdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_Pista_Empresa_IdEmpresa",
                table: "Pista",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pista_Empresa_IdEmpresa",
                table: "Pista");

            migrationBuilder.DropIndex(
                name: "IX_Pista_IdEmpresa",
                table: "Pista");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "PlanilhaExcelExportadas");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "PlanilhaExcelExportadas");

            migrationBuilder.DropColumn(
                name: "IdEmpresa",
                table: "Pista");

            migrationBuilder.DropColumn(
                name: "Cep",
                table: "Cliente");
        }
    }
}
