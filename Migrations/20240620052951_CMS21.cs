using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace courseManagementSystemV1.Migrations
{
    public partial class CMS21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_comments_commentParentID",
                table: "comments");

            migrationBuilder.AlterColumn<int>(
                name: "commentParentID",
                table: "comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_comments_commentParentID",
                table: "comments",
                column: "commentParentID",
                principalTable: "comments",
                principalColumn: "commentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_comments_commentParentID",
                table: "comments");

            migrationBuilder.AlterColumn<int>(
                name: "commentParentID",
                table: "comments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_comments_comments_commentParentID",
                table: "comments",
                column: "commentParentID",
                principalTable: "comments",
                principalColumn: "commentID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
