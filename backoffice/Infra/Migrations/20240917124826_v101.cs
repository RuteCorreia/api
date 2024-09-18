using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v101 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlvoBiologico_Cultura_IdCultura",
                table: "AlvoBiologico");

            migrationBuilder.DropForeignKey(
                name: "FK_AlvoBiologico_TipoDeUnidade_IdTipoDeUnidade",
                table: "AlvoBiologico");

            migrationBuilder.AlterColumn<int>(
                name: "IdTipoDeUnidade",
                table: "AlvoBiologico",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IdCultura",
                table: "AlvoBiologico",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AlvoBiologico_Cultura_IdCultura",
                table: "AlvoBiologico",
                column: "IdCultura",
                principalTable: "Cultura",
                principalColumn: "IdCultura");

            migrationBuilder.AddForeignKey(
                name: "FK_AlvoBiologico_TipoDeUnidade_IdTipoDeUnidade",
                table: "AlvoBiologico",
                column: "IdTipoDeUnidade",
                principalTable: "TipoDeUnidade",
                principalColumn: "Id");
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

            migrationBuilder.AlterColumn<int>(
                name: "IdTipoDeUnidade",
                table: "AlvoBiologico",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdCultura",
                table: "AlvoBiologico",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
    }
}
