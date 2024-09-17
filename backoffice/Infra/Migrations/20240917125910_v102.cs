using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v102 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnidadeProduto",
                table: "Bula");

            migrationBuilder.AddColumn<int>(
                name: "IdTipoDeUnidade",
                table: "Bula",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bula_IdTipoDeUnidade",
                table: "Bula",
                column: "IdTipoDeUnidade");

            migrationBuilder.AddForeignKey(
                name: "FK_Bula_TipoDeUnidade_IdTipoDeUnidade",
                table: "Bula",
                column: "IdTipoDeUnidade",
                principalTable: "TipoDeUnidade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bula_TipoDeUnidade_IdTipoDeUnidade",
                table: "Bula");

            migrationBuilder.DropIndex(
                name: "IX_Bula_IdTipoDeUnidade",
                table: "Bula");

            migrationBuilder.DropColumn(
                name: "IdTipoDeUnidade",
                table: "Bula");

            migrationBuilder.AddColumn<string>(
                name: "UnidadeProduto",
                table: "Bula",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
