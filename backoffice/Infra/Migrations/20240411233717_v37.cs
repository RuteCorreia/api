using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v37 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ManutencaoAeronaveItemsRevisao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Revisado = table.Column<bool>(type: "bit", nullable: false),
                    IdManutencaoAeronave = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManutencaoAeronaveItemsRevisao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManutencaoAeronaveItemsRevisao_ManutencaoAeronave_IdManutencaoAeronave",
                        column: x => x.IdManutencaoAeronave,
                        principalTable: "ManutencaoAeronave",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ManutencaoAeronaveItemsRevisao_IdManutencaoAeronave",
                table: "ManutencaoAeronaveItemsRevisao",
                column: "IdManutencaoAeronave");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ManutencaoAeronaveItemsRevisao");
        }
    }
}
