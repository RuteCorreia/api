using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v42 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdEmpresa",
                table: "Cliente",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_IdEmpresa",
                table: "Cliente",
                column: "IdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_Empresa_IdEmpresa",
                table: "Cliente",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_Empresa_IdEmpresa",
                table: "Cliente");

            migrationBuilder.DropIndex(
                name: "IX_Cliente_IdEmpresa",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "IdEmpresa",
                table: "Cliente");
        }
    }
}
