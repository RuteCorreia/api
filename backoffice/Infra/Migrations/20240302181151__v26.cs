using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class _v26 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ManutencaoAeronave",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAeronave = table.Column<int>(type: "int", nullable: true),
                    HorimetroInicial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HorasRevisao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HorasInspecao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Documento = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManutencaoAeronave", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManutencaoAeronave_Aeronave_IdAeronave",
                        column: x => x.IdAeronave,
                        principalTable: "Aeronave",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ManutencaoAeronave_IdAeronave",
                table: "ManutencaoAeronave",
                column: "IdAeronave");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ManutencaoAeronave");
        }
    }
}
