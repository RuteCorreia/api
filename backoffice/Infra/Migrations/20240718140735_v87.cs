using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v87 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "AlvoBiologico",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DoseProdutoPorHectare",
                table: "AlvoBiologico",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "IdCultura",
                table: "AlvoBiologico",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdTipoDeUnidade",
                table: "AlvoBiologico",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AlvoBiologico_IdCultura",
                table: "AlvoBiologico",
                column: "IdCultura");

            migrationBuilder.CreateIndex(
                name: "IX_AlvoBiologico_IdTipoDeUnidade",
                table: "AlvoBiologico",
                column: "IdTipoDeUnidade");

            migrationBuilder.AddForeignKey(
                name: "FK_AlvoBiologico_Cultura_IdCultura",
                table: "AlvoBiologico",
                column: "IdCultura",
                principalTable: "Cultura",
                principalColumn: "IdCultura",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AlvoBiologico_TipoDeUnidade_IdTipoDeUnidade",
                table: "AlvoBiologico",
                column: "IdTipoDeUnidade",
                principalTable: "TipoDeUnidade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlvoBiologico_Cultura_IdCultura",
                table: "AlvoBiologico");

            migrationBuilder.DropForeignKey(
                name: "FK_AlvoBiologico_TipoDeUnidade_IdTipoDeUnidade",
                table: "AlvoBiologico");

            migrationBuilder.DropIndex(
                name: "IX_AlvoBiologico_IdCultura",
                table: "AlvoBiologico");

            migrationBuilder.DropIndex(
                name: "IX_AlvoBiologico_IdTipoDeUnidade",
                table: "AlvoBiologico");

            migrationBuilder.DropColumn(
                name: "IdCultura",
                table: "AlvoBiologico");

            migrationBuilder.DropColumn(
                name: "IdTipoDeUnidade",
                table: "AlvoBiologico");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "AlvoBiologico",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DoseProdutoPorHectare",
                table: "AlvoBiologico",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
