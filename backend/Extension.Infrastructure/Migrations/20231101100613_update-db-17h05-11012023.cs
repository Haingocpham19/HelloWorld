using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Extension.Infracstructure.Migrations
{
    public partial class updatedb17h0511012023 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GetDateTime",
                table: "Currencies",
                newName: "CreatedDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Currencies",
                newName: "GetDateTime");
        }
    }
}
