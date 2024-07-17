using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v86 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isSelected",
                table: "RelatorioManutencaoRevisao",
                newName: "IsSelected");

            migrationBuilder.RenameColumn(
                name: "legenda",
                table: "RelatorioManutencaoComponenteImagem",
                newName: "Legenda");

            migrationBuilder.RenameColumn(
                name: "imagem",
                table: "RelatorioManutencaoComponenteImagem",
                newName: "Imagem");

            migrationBuilder.RenameColumn(
                name: "observacao",
                table: "RelatorioManutencaoComponente",
                newName: "Observacao");

            migrationBuilder.CreateTable(
                name: "Municipios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipios", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Municipios");

            migrationBuilder.RenameColumn(
                name: "IsSelected",
                table: "RelatorioManutencaoRevisao",
                newName: "isSelected");

            migrationBuilder.RenameColumn(
                name: "Legenda",
                table: "RelatorioManutencaoComponenteImagem",
                newName: "legenda");

            migrationBuilder.RenameColumn(
                name: "Imagem",
                table: "RelatorioManutencaoComponenteImagem",
                newName: "imagem");

            migrationBuilder.RenameColumn(
                name: "Observacao",
                table: "RelatorioManutencaoComponente",
                newName: "observacao");
        }
    }
}
