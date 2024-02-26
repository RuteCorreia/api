using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class v18 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Engenheiro_Empresa_IdEmpresa",
                table: "Engenheiro");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Engenheiro",
                table: "Engenheiro");


            migrationBuilder.DropColumn(
                    name: "IdEngenheiro",
                    table: "Engenheiro"
                );

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Engenheiro",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                    name: "PorcentagemComissao",
                    table: "Engenheiro",
                    type: "int",
                    nullable: false,
                    defaultValue: 0
                );

            migrationBuilder.AlterColumn<int>(
                name: "IdEmpresa",
                table: "Engenheiro",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Assinatura",
                table: "Engenheiro",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Engenheiro",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Engenheiro",
                table: "Engenheiro",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Engenheiro_Empresa_IdEmpresa",
                table: "Engenheiro",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Engenheiro_Empresa_IdEmpresa",
                table: "Engenheiro");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Engenheiro",
                table: "Engenheiro");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Engenheiro");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Engenheiro");

            migrationBuilder.RenameColumn(
                name: "PorcentagemComissao",
                table: "Engenheiro",
                newName: "IdEngenheiro");

            migrationBuilder.AlterColumn<int>(
                name: "IdEmpresa",
                table: "Engenheiro",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Assinatura",
                table: "Engenheiro",
                type: "varbinary(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "IdEngenheiro",
                table: "Engenheiro",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Engenheiro",
                table: "Engenheiro",
                column: "IdEngenheiro");

            migrationBuilder.AddForeignKey(
                name: "FK_Engenheiro_Empresa_IdEmpresa",
                table: "Engenheiro",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa");
        }
    }
}
