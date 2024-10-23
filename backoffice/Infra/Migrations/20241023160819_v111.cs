using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v111 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdEmpresa",
                table: "Cultura",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cultura_IdEmpresa",
                table: "Cultura",
                column: "IdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_Cultura_Empresa_IdEmpresa",
                table: "Cultura",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cultura_Empresa_IdEmpresa",
                table: "Cultura");

            migrationBuilder.DropIndex(
                name: "IX_Cultura_IdEmpresa",
                table: "Cultura");

            migrationBuilder.DropColumn(
                name: "IdEmpresa",
                table: "Cultura");
        }
    }
}
