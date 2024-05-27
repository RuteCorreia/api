using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class _v48 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImportacaoPlanilha",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IntPlanilhaImportada = table.Column<int>(type: "int", nullable: false),
                    Base64Planilha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QtdRegistroPlanilha = table.Column<int>(type: "int", nullable: false),
                    QtdRegistroBanco = table.Column<int>(type: "int", nullable: false),
                    IndexUltimaLinha = table.Column<int>(type: "int", nullable: false),
                    DadosSalvo = table.Column<bool>(type: "bit", nullable: false),
                    DataProcessamento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Erro = table.Column<bool>(type: "bit", nullable: false),
                    MenssagensProcessamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeCliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataPlanilha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportacaoPlanilha", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImportacaoPlanilha_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImportacaoPlanilha_IdUsuario",
                table: "ImportacaoPlanilha",
                column: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImportacaoPlanilha");
        }
    }
}
