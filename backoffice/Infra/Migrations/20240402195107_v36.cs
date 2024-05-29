using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v36 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ControleDeFrota_IdPiloto",
                table: "ControleDeFrota");

            migrationBuilder.DropIndex(
                name: "IX_CombateIncendio_IdExecutor",
                table: "CombateIncendio");

            migrationBuilder.DropIndex(
                name: "IX_Aplicacao_IdPiloto",
                table: "Aplicacao");

            migrationBuilder.DropIndex(
                name: "IX_Aplicacao_IdExecutor",
                table: "Aplicacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Aplicacao_Executor_IdExecutor",
                table: "Aplicacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Aplicacao_Piloto_IdPiloto",
                table: "Aplicacao");

            migrationBuilder.DropForeignKey(
                name: "FK_CombateIncendio_Executor_IdExecutor",
                table: "CombateIncendio");

            migrationBuilder.DropForeignKey(
                name: "FK_ControleDeFrota_Piloto_IdPiloto",
                table: "ControleDeFrota");

            migrationBuilder.DropColumn(
                name: "IdPiloto",
                table: "ControleDeFrota");

            migrationBuilder.DropColumn(
                name: "IdExecutor",
                table: "CombateIncendio");

            migrationBuilder.DropColumn(
                name: "IdPiloto",
                table: "Aplicacao");

            migrationBuilder.DropColumn(
               name: "IdExecutor",
               table: "Aplicacao");

            migrationBuilder.AddColumn<Guid>(
                name: "IdPiloto",
                table: "ControleDeFrota",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IdExecutor",
                table: "CombateIncendio",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IdPiloto",
                table: "Aplicacao",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IdExecutor",
                table: "Aplicacao",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Aplicacao_Usuario_IdExecutor",
                table: "Aplicacao",
                column: "IdExecutor",
                principalTable: "Usuario",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Aplicacao_Usuario_IdPiloto",
                table: "Aplicacao",
                column: "IdPiloto",
                principalTable: "Usuario",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CombateIncendio_Usuario_IdExecutor",
                table: "CombateIncendio",
                column: "IdExecutor",
                principalTable: "Usuario",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ControleDeFrota_Usuario_IdPiloto",
                table: "ControleDeFrota",
                column: "IdPiloto",
                principalTable: "Usuario",
                principalColumn: "Id");

            migrationBuilder.DropTable(
                name: "Engenheiro");

            migrationBuilder.DropTable(
                name: "Executor");

            migrationBuilder.DropTable(
                name: "Piloto");

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aplicacao_Usuario_IdExecutor",
                table: "Aplicacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Aplicacao_Usuario_IdPiloto",
                table: "Aplicacao");

            migrationBuilder.DropForeignKey(
                name: "FK_CombateIncendio_Usuario_IdExecutor",
                table: "CombateIncendio");

            migrationBuilder.DropForeignKey(
                name: "FK_ControleDeFrota_Usuario_IdPiloto",
                table: "ControleDeFrota");

            migrationBuilder.AlterColumn<int>(
                name: "IdPiloto",
                table: "ControleDeFrota",
                type: "int",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdExecutor",
                table: "CombateIncendio",
                type: "int",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdPiloto",
                table: "Aplicacao",
                type: "int",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdExecutor",
                table: "Aplicacao",
                type: "int",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Engenheiro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEmpresa = table.Column<int>(type: "int", nullable: false),
                    Assinatura = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CREA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PorcentagemComissao = table.Column<int>(type: "int", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engenheiro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Engenheiro_Empresa_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Executor",
                columns: table => new
                {
                    IdExecutor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEmpresa = table.Column<int>(type: "int", nullable: true),
                    Assinatura = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CFTA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Executor", x => x.IdExecutor);
                    table.ForeignKey(
                        name: "FK_Executor_Empresa_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa");
                });

            migrationBuilder.CreateTable(
                name: "Piloto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Assinatura = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CANAC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PorcentagemComissao = table.Column<int>(type: "int", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piloto", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Engenheiro_IdEmpresa",
                table: "Engenheiro",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_Executor_IdEmpresa",
                table: "Executor",
                column: "IdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_Aplicacao_Executor_IdExecutor",
                table: "Aplicacao",
                column: "IdExecutor",
                principalTable: "Executor",
                principalColumn: "IdExecutor");

            migrationBuilder.AddForeignKey(
                name: "FK_Aplicacao_Piloto_IdPiloto",
                table: "Aplicacao",
                column: "IdPiloto",
                principalTable: "Piloto",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CombateIncendio_Executor_IdExecutor",
                table: "CombateIncendio",
                column: "IdExecutor",
                principalTable: "Executor",
                principalColumn: "IdExecutor");

            migrationBuilder.AddForeignKey(
                name: "FK_ControleDeFrota_Piloto_IdPiloto",
                table: "ControleDeFrota",
                column: "IdPiloto",
                principalTable: "Piloto",
                principalColumn: "Id");
        }
    }
}
