using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Extension.Infracstructure.Migrations
{
    public partial class updatedb17h1711012023 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ClientCards_ClientCardId1",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ClientCardId1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ClientCardId1",
                table: "Products");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientCardId",
                table: "Products",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ClientCardId",
                table: "Products",
                column: "ClientCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ClientCards_ClientCardId",
                table: "Products",
                column: "ClientCardId",
                principalTable: "ClientCards",
                principalColumn: "ClientCardId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ClientCards_ClientCardId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ClientCardId",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "ClientCardId",
                table: "Products",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "ClientCardId1",
                table: "Products",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ClientCardId1",
                table: "Products",
                column: "ClientCardId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ClientCards_ClientCardId1",
                table: "Products",
                column: "ClientCardId1",
                principalTable: "ClientCards",
                principalColumn: "ClientCardId");
        }
    }
}
