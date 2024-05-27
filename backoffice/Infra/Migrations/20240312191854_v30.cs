using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v30 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Credencial",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Funcao",
                table: "Usuario");

            migrationBuilder.CreateTable(
                name: "UsuarioCredencial",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Credencial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Funcao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioCredencial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioCredencial_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioCredencial_IdUsuario",
                table: "UsuarioCredencial",
                column: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioCredencial");

            migrationBuilder.AddColumn<string>(
                name: "Credencial",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Funcao",
                table: "Usuario",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
