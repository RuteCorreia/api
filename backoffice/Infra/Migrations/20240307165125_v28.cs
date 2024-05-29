using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v28 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Credencial",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Funcao",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IdEmpresa",
                table: "Usuario",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PrimeiroAcesso",
                table: "Usuario",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdEmpresa",
                table: "Usuario",
                column: "IdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Empresa_IdEmpresa",
                table: "Usuario",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Empresa_IdEmpresa",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_IdEmpresa",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Credencial",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Funcao",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "IdEmpresa",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "PrimeiroAcesso",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Usuario");
        }
    }
}
