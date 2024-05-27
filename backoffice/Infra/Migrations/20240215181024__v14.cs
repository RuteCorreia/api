using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class _v14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    MenuItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Route = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.MenuItemId);
                });

            migrationBuilder.CreateTable(
                name: "MenuUsuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdMenu = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuUsuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuUsuario_AspNetUsers_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuUsuario_Menu_IdMenu",
                        column: x => x.IdMenu,
                        principalTable: "Menu",
                        principalColumn: "MenuItemId");
                });

            migrationBuilder.CreateTable(
                name: "SubMenu",
                columns: table => new
                {
                    SubMenuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Route = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MenuItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubMenu", x => x.SubMenuId);
                    table.ForeignKey(
                        name: "FK_SubMenu_Menu_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "Menu",
                        principalColumn: "MenuItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuUsuario_IdMenu",
                table: "MenuUsuario",
                column: "IdMenu");

            migrationBuilder.CreateIndex(
                name: "IX_MenuUsuario_IdUsuario",
                table: "MenuUsuario",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_SubMenu_MenuItemId",
                table: "SubMenu",
                column: "MenuItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuUsuario");

            migrationBuilder.DropTable(
                name: "SubMenu");

            migrationBuilder.DropTable(
                name: "Menu");
        }
    }
}
