using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v57 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "assinaturaResponsavel",
                table: "DadosResponsavel",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UF",
                table: "DadosResponsavel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "DadosResponsavel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Documento",
                table: "DadosResponsavel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Data",
                table: "DadosResponsavel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Cidade",
                table: "DadosResponsavel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "PostoGraduacao",
                table: "DadosResponsavel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Re",
                table: "DadosResponsavel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LocalIncendio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Long = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Referencia = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalIncendio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RelatorioIncendio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PistaId = table.Column<int>(type: "int", nullable: false),
                    LocalIncendioId = table.Column<int>(type: "int", nullable: false),
                    DecolagemPousoFirefighting = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DadosResponsavelId = table.Column<int>(type: "int", nullable: false),
                    CoordenadorBaseOperacionalId = table.Column<int>(type: "int", nullable: true),
                    ComandanteOcorrenciaId = table.Column<int>(type: "int", nullable: true),
                    ContratoPrestacaoServicoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatorioIncendio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelatorioIncendio_ContratoPrestacaoServico_ContratoPrestacaoServicoId",
                        column: x => x.ContratoPrestacaoServicoId,
                        principalTable: "ContratoPrestacaoServico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RelatorioIncendio_DadosResponsavel_ComandanteOcorrenciaId",
                        column: x => x.ComandanteOcorrenciaId,
                        principalTable: "DadosResponsavel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RelatorioIncendio_DadosResponsavel_CoordenadorBaseOperacionalId",
                        column: x => x.CoordenadorBaseOperacionalId,
                        principalTable: "DadosResponsavel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RelatorioIncendio_DadosResponsavel_DadosResponsavelId",
                        column: x => x.DadosResponsavelId,
                        principalTable: "DadosResponsavel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RelatorioIncendio_LocalIncendio_LocalIncendioId",
                        column: x => x.LocalIncendioId,
                        principalTable: "LocalIncendio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RelatorioIncendio_Pista_PistaId",
                        column: x => x.PistaId,
                        principalTable: "Pista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RelatorioIncendio_ComandanteOcorrenciaId",
                table: "RelatorioIncendio",
                column: "ComandanteOcorrenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatorioIncendio_ContratoPrestacaoServicoId",
                table: "RelatorioIncendio",
                column: "ContratoPrestacaoServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatorioIncendio_CoordenadorBaseOperacionalId",
                table: "RelatorioIncendio",
                column: "CoordenadorBaseOperacionalId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatorioIncendio_DadosResponsavelId",
                table: "RelatorioIncendio",
                column: "DadosResponsavelId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatorioIncendio_LocalIncendioId",
                table: "RelatorioIncendio",
                column: "LocalIncendioId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatorioIncendio_PistaId",
                table: "RelatorioIncendio",
                column: "PistaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RelatorioIncendio");

            migrationBuilder.DropTable(
                name: "LocalIncendio");

            migrationBuilder.DropColumn(
                name: "PostoGraduacao",
                table: "DadosResponsavel");

            migrationBuilder.DropColumn(
                name: "Re",
                table: "DadosResponsavel");

            migrationBuilder.AlterColumn<byte[]>(
                name: "assinaturaResponsavel",
                table: "DadosResponsavel",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UF",
                table: "DadosResponsavel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "DadosResponsavel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Documento",
                table: "DadosResponsavel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Data",
                table: "DadosResponsavel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cidade",
                table: "DadosResponsavel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
