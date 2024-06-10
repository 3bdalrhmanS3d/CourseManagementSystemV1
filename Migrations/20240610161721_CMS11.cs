using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace courseManagementSystemV1.Migrations
{
    public partial class CMS11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    commentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    commentText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    commentphoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    commentdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    commentUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    commentParentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comments", x => x.commentID);
                    table.ForeignKey(
                        name: "FK_comments_comments_commentParentID",
                        column: x => x.commentParentID,
                        principalTable: "comments",
                        principalColumn: "commentID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_comments_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_comments_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_comments_commentParentID",
                table: "comments",
                column: "commentParentID");

            migrationBuilder.CreateIndex(
                name: "IX_comments_CourseID",
                table: "comments",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_comments_UserID",
                table: "comments",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comments");
        }
    }
}
