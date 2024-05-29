using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Assinatura",
                table: "Engenheiro",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CREA",
                table: "Engenheiro",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Engenheiro",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IdEmpresa",
                table: "Engenheiro",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Engenheiro",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PorcentagemComissao",
                table: "Engenheiro",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "Engenheiro",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Engenheiro",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Engenheiro_IdEmpresa",
                table: "Engenheiro",
                column: "IdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_Engenheiro_Empresa_IdEmpresa",
                table: "Engenheiro",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Engenheiro_Empresa_IdEmpresa",
                table: "Engenheiro");

            migrationBuilder.DropIndex(
                name: "IX_Engenheiro_IdEmpresa",
                table: "Engenheiro");

            migrationBuilder.DropColumn(
                name: "Assinatura",
                table: "Engenheiro");

            migrationBuilder.DropColumn(
                name: "CREA",
                table: "Engenheiro");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Engenheiro");

            migrationBuilder.DropColumn(
                name: "IdEmpresa",
                table: "Engenheiro");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Engenheiro");

            migrationBuilder.DropColumn(
                name: "PorcentagemComissao",
                table: "Engenheiro");

            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Engenheiro");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Engenheiro");
        }
    }
}
