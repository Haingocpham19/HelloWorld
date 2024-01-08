using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Extension.Infracstructure.Migrations
{
    public partial class addfullauditlog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Currencies",
                newName: "CreationTime");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "ClientCards",
                newName: "CreationTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "SourcePages",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "SourcePages",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "SourcePages",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "SourcePages",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SourcePages",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "SourcePages",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "SourcePages",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Products",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "Products",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "Products",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Products",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Products",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "Products",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "Products",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "Currencies",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "Currencies",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Currencies",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Currencies",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "Currencies",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "Currencies",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "ClientCards",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "ClientCards",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "ClientCards",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ClientCards",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "ClientCards",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "ClientCards",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "SourcePages");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "SourcePages");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "SourcePages");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "SourcePages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SourcePages");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "SourcePages");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "SourcePages");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "ClientCards");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "ClientCards");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "ClientCards");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ClientCards");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "ClientCards");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "ClientCards");

            migrationBuilder.RenameColumn(
                name: "CreationTime",
                table: "Currencies",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "CreationTime",
                table: "ClientCards",
                newName: "CreateDate");
        }
    }
}
