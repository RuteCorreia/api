using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class _v8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BulaAplicacao",
                columns: table => new
                {
                    IdBulaAplicacao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCultura = table.Column<int>(type: "int", nullable: true),
                    IdAlvoBiologico = table.Column<int>(type: "int", nullable: true),
                    IdBula = table.Column<int>(type: "int", nullable: true),
                    DoseProdutoComercial = table.Column<int>(type: "int", nullable: true),
                    TipoDeUnidade = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BulaAplicacao", x => x.IdBulaAplicacao);
                    table.ForeignKey(
                        name: "FK_BulaAplicacao_AlvoBiologico_IdAlvoBiologico",
                        column: x => x.IdAlvoBiologico,
                        principalTable: "AlvoBiologico",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BulaAplicacao_Bula_IdBula",
                        column: x => x.IdBula,
                        principalTable: "Bula",
                        principalColumn: "IdBula");
                    table.ForeignKey(
                        name: "FK_BulaAplicacao_Cultura_IdCultura",
                        column: x => x.IdCultura,
                        principalTable: "Cultura",
                        principalColumn: "IdCultura");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BulaAplicacao_IdAlvoBiologico",
                table: "BulaAplicacao",
                column: "IdAlvoBiologico");

            migrationBuilder.CreateIndex(
                name: "IX_BulaAplicacao_IdBula",
                table: "BulaAplicacao",
                column: "IdBula");

            migrationBuilder.CreateIndex(
                name: "IX_BulaAplicacao_IdCultura",
                table: "BulaAplicacao",
                column: "IdCultura");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BulaAplicacao");
        }
    }
}
