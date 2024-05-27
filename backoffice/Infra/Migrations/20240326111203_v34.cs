using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v34 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubMenu_Menu_MenuItemId",
                table: "SubMenu");

            migrationBuilder.AlterColumn<int>(
                name: "MenuItemId",
                table: "SubMenu",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "SubMenuItemId",
                table: "SubMenu",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubMenu_SubMenuItemId",
                table: "SubMenu",
                column: "SubMenuItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubMenu_Menu_MenuItemId",
                table: "SubMenu",
                column: "MenuItemId",
                principalTable: "Menu",
                principalColumn: "MenuItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubMenu_SubMenu_SubMenuItemId",
                table: "SubMenu",
                column: "SubMenuItemId",
                principalTable: "SubMenu",
                principalColumn: "SubMenuId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubMenu_Menu_MenuItemId",
                table: "SubMenu");

            migrationBuilder.DropForeignKey(
                name: "FK_SubMenu_SubMenu_SubMenuItemId",
                table: "SubMenu");

            migrationBuilder.DropIndex(
                name: "IX_SubMenu_SubMenuItemId",
                table: "SubMenu");

            migrationBuilder.DropColumn(
                name: "SubMenuItemId",
                table: "SubMenu");

            migrationBuilder.AlterColumn<int>(
                name: "MenuItemId",
                table: "SubMenu",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SubMenu_Menu_MenuItemId",
                table: "SubMenu",
                column: "MenuItemId",
                principalTable: "Menu",
                principalColumn: "MenuItemId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
