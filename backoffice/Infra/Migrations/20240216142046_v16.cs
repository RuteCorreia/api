using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlanoContratado",
                table: "Empresa");

            migrationBuilder.AddColumn<string>(
                name: "CNPJ",
                table: "Empresa",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "Empresa",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Empresa",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Endereco",
                table: "Empresa",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Empresa",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InscricaoEstadual",
                table: "Empresa",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NrCDA",
                table: "Empresa",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Numero",
                table: "Empresa",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegistroMapa",
                table: "Empresa",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Empresa",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CNPJ",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "Endereco",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "InscricaoEstadual",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "NrCDA",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "Numero",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "RegistroMapa",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Empresa");

            migrationBuilder.AddColumn<int>(
                name: "PlanoContratado",
                table: "Empresa",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
