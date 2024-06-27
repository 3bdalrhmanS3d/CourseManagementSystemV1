using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace courseManagementSystemV1.Migrations
{
    public partial class CMS20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShowHiddenComment",
                table: "comments",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "hideComment",
                table: "comments",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "whoHideComment",
                table: "comments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "whoShowHiddenComment",
                table: "comments",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowHiddenComment",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "hideComment",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "whoHideComment",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "whoShowHiddenComment",
                table: "comments");
        }
    }
}
