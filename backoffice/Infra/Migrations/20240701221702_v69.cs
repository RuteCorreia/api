using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v69 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CombateIncendioPista",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HorarioChegadaPista = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HorimetroChegadaPista = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodigoICAOPista = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomePista = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LatPista = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LongPista = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CombateIncendioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombateIncendioPista", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CombateIncendioPista_CombateIncendio_CombateIncendioId",
                        column: x => x.CombateIncendioId,
                        principalTable: "CombateIncendio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CombateIncendioPista_CombateIncendioId",
                table: "CombateIncendioPista",
                column: "CombateIncendioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CombateIncendioPista");
        }
    }
}
