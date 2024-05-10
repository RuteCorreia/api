using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class _v40 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RelatorioAplicacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContratanteId = table.Column<int>(type: "int", nullable: false),
                    IdentificacaoAreaTratadaId = table.Column<int>(type: "int", nullable: false),
                    CaracteristicasProdutoAplicadoId = table.Column<int>(type: "int", nullable: false),
                    RecomendacoesTecnicasId = table.Column<int>(type: "int", nullable: false),
                    RelatorioAplicacaoId = table.Column<int>(type: "int", nullable: false),
                    ContratoPrestacaoServicoId = table.Column<int>(type: "int", nullable: false),
                    DadosResponsavelId = table.Column<int>(type: "int", nullable: false),
                    Piloto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Executor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefDocument = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatorioAplicacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelatorioAplicacao_AplicacaoRecomendacoesTecnicas_RelatorioAplicacaoId",
                        column: x => x.RelatorioAplicacaoId,
                        principalTable: "AplicacaoRecomendacoesTecnicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RelatorioAplicacao_RelatorioAplicacaoId",
                table: "RelatorioAplicacao",
                column: "RelatorioAplicacaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RelatorioAplicacao");
        }
    }
}
