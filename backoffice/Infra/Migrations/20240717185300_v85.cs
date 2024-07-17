using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v85 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RelatorioManutencao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsMapa = table.Column<bool>(type: "bit", nullable: false),
                    IdEmpresa = table.Column<int>(type: "int", nullable: true),
                    NomeRelatorio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusEnvio = table.Column<int>(type: "int", nullable: true),
                    IdAeronave = table.Column<int>(type: "int", nullable: true),
                    Horimetro = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatorioManutencao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelatorioManutencao_Aeronave_IdAeronave",
                        column: x => x.IdAeronave,
                        principalTable: "Aeronave",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RelatorioManutencao_Empresa_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa");
                });

            migrationBuilder.CreateTable(
                name: "RelatorioManutencaoComponente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEmpresa = table.Column<int>(type: "int", nullable: true),
                    IdRelatorioManutencao = table.Column<int>(type: "int", nullable: true),
                    IdComponente = table.Column<int>(type: "int", nullable: true),
                    observacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdManutencaoAeronaveItemsRevisao = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatorioManutencaoComponente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelatorioManutencaoComponente_Componente_IdComponente",
                        column: x => x.IdComponente,
                        principalTable: "Componente",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RelatorioManutencaoComponente_Empresa_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa");
                    table.ForeignKey(
                        name: "FK_RelatorioManutencaoComponente_ManutencaoAeronaveItemsRevisao_IdManutencaoAeronaveItemsRevisao",
                        column: x => x.IdManutencaoAeronaveItemsRevisao,
                        principalTable: "ManutencaoAeronaveItemsRevisao",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RelatorioManutencaoComponente_RelatorioManutencao_IdRelatorioManutencao",
                        column: x => x.IdRelatorioManutencao,
                        principalTable: "RelatorioManutencao",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RelatorioManutencaoRevisao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEmpresa = table.Column<int>(type: "int", nullable: true),
                    IdRelatorioManutencao = table.Column<int>(type: "int", nullable: true),
                    isSelected = table.Column<bool>(type: "bit", nullable: false),
                    IdManutencaoAeronaveItemsRevisao = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatorioManutencaoRevisao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelatorioManutencaoRevisao_Empresa_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa");
                    table.ForeignKey(
                        name: "FK_RelatorioManutencaoRevisao_ManutencaoAeronaveItemsRevisao_IdManutencaoAeronaveItemsRevisao",
                        column: x => x.IdManutencaoAeronaveItemsRevisao,
                        principalTable: "ManutencaoAeronaveItemsRevisao",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RelatorioManutencaoRevisao_RelatorioManutencao_IdRelatorioManutencao",
                        column: x => x.IdRelatorioManutencao,
                        principalTable: "RelatorioManutencao",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RelatorioManutencaoComponenteImagem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEmpresa = table.Column<int>(type: "int", nullable: true),
                    IdRelatorioManutencaoComponente = table.Column<int>(type: "int", nullable: true),
                    legenda = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    imagem = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatorioManutencaoComponenteImagem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelatorioManutencaoComponenteImagem_Empresa_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa");
                    table.ForeignKey(
                        name: "FK_RelatorioManutencaoComponenteImagem_RelatorioManutencaoComponente_IdRelatorioManutencaoComponente",
                        column: x => x.IdRelatorioManutencaoComponente,
                        principalTable: "RelatorioManutencaoComponente",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RelatorioManutencao_IdAeronave",
                table: "RelatorioManutencao",
                column: "IdAeronave");

            migrationBuilder.CreateIndex(
                name: "IX_RelatorioManutencao_IdEmpresa",
                table: "RelatorioManutencao",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_RelatorioManutencaoComponente_IdComponente",
                table: "RelatorioManutencaoComponente",
                column: "IdComponente");

            migrationBuilder.CreateIndex(
                name: "IX_RelatorioManutencaoComponente_IdEmpresa",
                table: "RelatorioManutencaoComponente",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_RelatorioManutencaoComponente_IdManutencaoAeronaveItemsRevisao",
                table: "RelatorioManutencaoComponente",
                column: "IdManutencaoAeronaveItemsRevisao");

            migrationBuilder.CreateIndex(
                name: "IX_RelatorioManutencaoComponente_IdRelatorioManutencao",
                table: "RelatorioManutencaoComponente",
                column: "IdRelatorioManutencao");

            migrationBuilder.CreateIndex(
                name: "IX_RelatorioManutencaoComponenteImagem_IdEmpresa",
                table: "RelatorioManutencaoComponenteImagem",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_RelatorioManutencaoComponenteImagem_IdRelatorioManutencaoComponente",
                table: "RelatorioManutencaoComponenteImagem",
                column: "IdRelatorioManutencaoComponente");

            migrationBuilder.CreateIndex(
                name: "IX_RelatorioManutencaoRevisao_IdEmpresa",
                table: "RelatorioManutencaoRevisao",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_RelatorioManutencaoRevisao_IdManutencaoAeronaveItemsRevisao",
                table: "RelatorioManutencaoRevisao",
                column: "IdManutencaoAeronaveItemsRevisao");

            migrationBuilder.CreateIndex(
                name: "IX_RelatorioManutencaoRevisao_IdRelatorioManutencao",
                table: "RelatorioManutencaoRevisao",
                column: "IdRelatorioManutencao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RelatorioManutencaoComponenteImagem");

            migrationBuilder.DropTable(
                name: "RelatorioManutencaoRevisao");

            migrationBuilder.DropTable(
                name: "RelatorioManutencaoComponente");

            migrationBuilder.DropTable(
                name: "RelatorioManutencao");
        }
    }
}
