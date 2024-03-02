using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class _v25 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Componente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAeronave = table.Column<int>(type: "int", nullable: true),
                    NomeComponente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grupo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TLV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TBO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnumTLV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnumTBO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaInspecao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrazoParaInspecao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TSN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TSO = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Componente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Componente_Aeronave_IdAeronave",
                        column: x => x.IdAeronave,
                        principalTable: "Aeronave",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Componente_IdAeronave",
                table: "Componente",
                column: "IdAeronave");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Componente");
        }
    }
}
