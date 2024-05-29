using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class _v15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuUsuario_Menu_IdMenu",
                table: "MenuUsuario");

            migrationBuilder.RenameColumn(
                name: "IdMenu",
                table: "MenuUsuario",
                newName: "IdSubMenu");

            migrationBuilder.RenameIndex(
                name: "IX_MenuUsuario_IdMenu",
                table: "MenuUsuario",
                newName: "IX_MenuUsuario_IdSubMenu");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuUsuario_SubMenu_IdSubMenu",
                table: "MenuUsuario",
                column: "IdSubMenu",
                principalTable: "SubMenu",
                principalColumn: "SubMenuId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuUsuario_SubMenu_IdSubMenu",
                table: "MenuUsuario");

            migrationBuilder.RenameColumn(
                name: "IdSubMenu",
                table: "MenuUsuario",
                newName: "IdMenu");

            migrationBuilder.RenameIndex(
                name: "IX_MenuUsuario_IdSubMenu",
                table: "MenuUsuario",
                newName: "IX_MenuUsuario_IdMenu");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuUsuario_Menu_IdMenu",
                table: "MenuUsuario",
                column: "IdMenu",
                principalTable: "Menu",
                principalColumn: "MenuItemId");
        }
    }
}
