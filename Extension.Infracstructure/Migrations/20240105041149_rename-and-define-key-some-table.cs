using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Extension.Infracstructure.Migrations
{
    public partial class renameanddefinekeysometable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "SourcePageId",
                table: "SourcePages",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CurencyId",
                table: "Currencies",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ClientCardId",
                table: "ClientCards",
                newName: "Id");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SourcePages",
                newName: "SourcePageId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Currencies",
                newName: "CurencyId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ClientCards",
                newName: "ClientCardId");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "ProductId");
        }
    }
}
