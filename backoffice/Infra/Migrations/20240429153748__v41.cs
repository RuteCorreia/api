using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class _v41 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RelatorioAplicacao_AplicacaoRecomendacoesTecnicas_RelatorioAplicacaoId",
                table: "RelatorioAplicacao");

            migrationBuilder.DropIndex(
                name: "IX_RelatorioAplicacao_RelatorioAplicacaoId",
                table: "RelatorioAplicacao");

            migrationBuilder.AlterColumn<int>(
                name: "RelatorioAplicacaoId",
                table: "RelatorioAplicacao",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RecomendacoesTecnicasId",
                table: "RelatorioAplicacao",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IdentificacaoAreaTratadaId",
                table: "RelatorioAplicacao",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DadosResponsavelId",
                table: "RelatorioAplicacao",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ContratoPrestacaoServicoId",
                table: "RelatorioAplicacao",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ContratanteId",
                table: "RelatorioAplicacao",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CaracteristicasProdutoAplicadoId",
                table: "RelatorioAplicacao",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "CaracteristicasProdutoAplicado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cultura = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiturarioAgronomico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeProduto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassificacaoToxicologica = table.Column<int>(type: "int", nullable: true),
                    Classe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoFormulacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlvoBiologico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoseProdutoHectare = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnidadeDoseProdutoHectare = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adjuvante = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoServico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroReceituarioAgronomico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataEmissao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaracteristicasProdutoAplicado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contratante",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoContratante = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RG = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CNPJ = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InscricaoEstadual = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contratante", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContratoPrestacaoServico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistanciaPista = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Preco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnidadePreco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Extensao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorTotal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vencimento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomePiloto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Executor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContratoPrestacaoServico", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DadosResponsavel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Documento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    assinaturaResponsavel = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadosResponsavel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentificacaoAreaTratada",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Localizacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cultura = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Extensao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CroquiArea = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentificacaoAreaTratada", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RelatorioAplicacao_CaracteristicasProdutoAplicadoId",
                table: "RelatorioAplicacao",
                column: "CaracteristicasProdutoAplicadoId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatorioAplicacao_ContratanteId",
                table: "RelatorioAplicacao",
                column: "ContratanteId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatorioAplicacao_ContratoPrestacaoServicoId",
                table: "RelatorioAplicacao",
                column: "ContratoPrestacaoServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatorioAplicacao_DadosResponsavelId",
                table: "RelatorioAplicacao",
                column: "DadosResponsavelId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatorioAplicacao_IdentificacaoAreaTratadaId",
                table: "RelatorioAplicacao",
                column: "IdentificacaoAreaTratadaId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatorioAplicacao_RecomendacoesTecnicasId",
                table: "RelatorioAplicacao",
                column: "RecomendacoesTecnicasId");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatorioAplicacao_AplicacaoRecomendacoesTecnicas_RecomendacoesTecnicasId",
                table: "RelatorioAplicacao",
                column: "RecomendacoesTecnicasId",
                principalTable: "AplicacaoRecomendacoesTecnicas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatorioAplicacao_CaracteristicasProdutoAplicado_CaracteristicasProdutoAplicadoId",
                table: "RelatorioAplicacao",
                column: "CaracteristicasProdutoAplicadoId",
                principalTable: "CaracteristicasProdutoAplicado",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatorioAplicacao_Contratante_ContratanteId",
                table: "RelatorioAplicacao",
                column: "ContratanteId",
                principalTable: "Contratante",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatorioAplicacao_ContratoPrestacaoServico_ContratoPrestacaoServicoId",
                table: "RelatorioAplicacao",
                column: "ContratoPrestacaoServicoId",
                principalTable: "ContratoPrestacaoServico",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatorioAplicacao_DadosResponsavel_DadosResponsavelId",
                table: "RelatorioAplicacao",
                column: "DadosResponsavelId",
                principalTable: "DadosResponsavel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatorioAplicacao_IdentificacaoAreaTratada_IdentificacaoAreaTratadaId",
                table: "RelatorioAplicacao",
                column: "IdentificacaoAreaTratadaId",
                principalTable: "IdentificacaoAreaTratada",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RelatorioAplicacao_AplicacaoRecomendacoesTecnicas_RecomendacoesTecnicasId",
                table: "RelatorioAplicacao");

            migrationBuilder.DropForeignKey(
                name: "FK_RelatorioAplicacao_CaracteristicasProdutoAplicado_CaracteristicasProdutoAplicadoId",
                table: "RelatorioAplicacao");

            migrationBuilder.DropForeignKey(
                name: "FK_RelatorioAplicacao_Contratante_ContratanteId",
                table: "RelatorioAplicacao");

            migrationBuilder.DropForeignKey(
                name: "FK_RelatorioAplicacao_ContratoPrestacaoServico_ContratoPrestacaoServicoId",
                table: "RelatorioAplicacao");

            migrationBuilder.DropForeignKey(
                name: "FK_RelatorioAplicacao_DadosResponsavel_DadosResponsavelId",
                table: "RelatorioAplicacao");

            migrationBuilder.DropForeignKey(
                name: "FK_RelatorioAplicacao_IdentificacaoAreaTratada_IdentificacaoAreaTratadaId",
                table: "RelatorioAplicacao");

            migrationBuilder.DropTable(
                name: "CaracteristicasProdutoAplicado");

            migrationBuilder.DropTable(
                name: "Contratante");

            migrationBuilder.DropTable(
                name: "ContratoPrestacaoServico");

            migrationBuilder.DropTable(
                name: "DadosResponsavel");

            migrationBuilder.DropTable(
                name: "IdentificacaoAreaTratada");

            migrationBuilder.DropIndex(
                name: "IX_RelatorioAplicacao_CaracteristicasProdutoAplicadoId",
                table: "RelatorioAplicacao");

            migrationBuilder.DropIndex(
                name: "IX_RelatorioAplicacao_ContratanteId",
                table: "RelatorioAplicacao");

            migrationBuilder.DropIndex(
                name: "IX_RelatorioAplicacao_ContratoPrestacaoServicoId",
                table: "RelatorioAplicacao");

            migrationBuilder.DropIndex(
                name: "IX_RelatorioAplicacao_DadosResponsavelId",
                table: "RelatorioAplicacao");

            migrationBuilder.DropIndex(
                name: "IX_RelatorioAplicacao_IdentificacaoAreaTratadaId",
                table: "RelatorioAplicacao");

            migrationBuilder.DropIndex(
                name: "IX_RelatorioAplicacao_RecomendacoesTecnicasId",
                table: "RelatorioAplicacao");

            migrationBuilder.AlterColumn<int>(
                name: "RelatorioAplicacaoId",
                table: "RelatorioAplicacao",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RecomendacoesTecnicasId",
                table: "RelatorioAplicacao",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdentificacaoAreaTratadaId",
                table: "RelatorioAplicacao",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DadosResponsavelId",
                table: "RelatorioAplicacao",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ContratoPrestacaoServicoId",
                table: "RelatorioAplicacao",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ContratanteId",
                table: "RelatorioAplicacao",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CaracteristicasProdutoAplicadoId",
                table: "RelatorioAplicacao",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RelatorioAplicacao_RelatorioAplicacaoId",
                table: "RelatorioAplicacao",
                column: "RelatorioAplicacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatorioAplicacao_AplicacaoRecomendacoesTecnicas_RelatorioAplicacaoId",
                table: "RelatorioAplicacao",
                column: "RelatorioAplicacaoId",
                principalTable: "AplicacaoRecomendacoesTecnicas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
