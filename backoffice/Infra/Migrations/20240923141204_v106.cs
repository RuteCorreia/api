using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v106 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FrotaBaterias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdBateria = table.Column<int>(type: "int", nullable: true),
                    IdFrota = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CicloInicial = table.Column<int>(type: "int", nullable: true),
                    CicloFinal = table.Column<int>(type: "int", nullable: true),
                    IdEmpresa = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrotaBaterias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FrotaBaterias_Baterias_IdBateria",
                        column: x => x.IdBateria,
                        principalTable: "Baterias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FrotaBaterias_ControleDeFrota_IdFrota",
                        column: x => x.IdFrota,
                        principalTable: "ControleDeFrota",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FrotaBaterias_Empresa_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa");
                });

            migrationBuilder.CreateTable(
                name: "FrotaGeradores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdGerador = table.Column<int>(type: "int", nullable: true),
                    IdFrota = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HoraInicio = table.Column<int>(type: "int", nullable: true),
                    HoraFim = table.Column<int>(type: "int", nullable: true),
                    HorasUso = table.Column<int>(type: "int", nullable: true),
                    DataTrocaOleo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdEmpresa = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrotaGeradores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FrotaGeradores_ControleDeFrota_IdFrota",
                        column: x => x.IdFrota,
                        principalTable: "ControleDeFrota",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FrotaGeradores_Empresa_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa");
                    table.ForeignKey(
                        name: "FK_FrotaGeradores_Geradores_IdGerador",
                        column: x => x.IdGerador,
                        principalTable: "Geradores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FrotaMotobombas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdFrota = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Identificacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LitrosOleo = table.Column<double>(type: "float", nullable: true),
                    LitrosGasolina = table.Column<double>(type: "float", nullable: true),
                    CheckList = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdEmpresa = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrotaMotobombas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FrotaMotobombas_ControleDeFrota_IdFrota",
                        column: x => x.IdFrota,
                        principalTable: "ControleDeFrota",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FrotaMotobombas_Empresa_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FrotaBaterias_IdBateria",
                table: "FrotaBaterias",
                column: "IdBateria");

            migrationBuilder.CreateIndex(
                name: "IX_FrotaBaterias_IdEmpresa",
                table: "FrotaBaterias",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_FrotaBaterias_IdFrota",
                table: "FrotaBaterias",
                column: "IdFrota");

            migrationBuilder.CreateIndex(
                name: "IX_FrotaGeradores_IdEmpresa",
                table: "FrotaGeradores",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_FrotaGeradores_IdFrota",
                table: "FrotaGeradores",
                column: "IdFrota");

            migrationBuilder.CreateIndex(
                name: "IX_FrotaGeradores_IdGerador",
                table: "FrotaGeradores",
                column: "IdGerador");

            migrationBuilder.CreateIndex(
                name: "IX_FrotaMotobombas_IdEmpresa",
                table: "FrotaMotobombas",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_FrotaMotobombas_IdFrota",
                table: "FrotaMotobombas",
                column: "IdFrota");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FrotaBaterias");

            migrationBuilder.DropTable(
                name: "FrotaGeradores");

            migrationBuilder.DropTable(
                name: "FrotaMotobombas");
        }
    }
}
