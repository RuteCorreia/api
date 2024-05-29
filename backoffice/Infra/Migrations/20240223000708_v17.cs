using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Piloto_Empresa_IdEmpresa",
                table: "Piloto");

            migrationBuilder.DropIndex(
                name: "IX_Piloto_IdEmpresa",
                table: "Piloto");

            migrationBuilder.DropColumn(
                name: "IdEmpresa",
                table: "Piloto");

            migrationBuilder.RenameColumn(
                name: "NomePiloto",
                table: "Piloto",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "CDAC",
                table: "Piloto",
                newName: "CANAC");

            migrationBuilder.RenameColumn(
                name: "IdPiloto",
                table: "Piloto",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "PorcentagemComissao",
                table: "Piloto",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Assinatura",
                table: "Piloto",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Piloto",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Piloto");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Piloto",
                newName: "NomePiloto");

            migrationBuilder.RenameColumn(
                name: "CANAC",
                table: "Piloto",
                newName: "CDAC");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Piloto",
                newName: "IdPiloto");

            migrationBuilder.AlterColumn<string>(
                name: "PorcentagemComissao",
                table: "Piloto",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Assinatura",
                table: "Piloto",
                type: "varbinary(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "IdEmpresa",
                table: "Piloto",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Piloto_IdEmpresa",
                table: "Piloto",
                column: "IdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_Piloto_Empresa_IdEmpresa",
                table: "Piloto",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa");
        }
    }
}
