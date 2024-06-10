using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace courseManagementSystemV1.Migrations
{
    public partial class CMS12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "courseSpecificQuestions",
                columns: table => new
                {
                    CourseSpecificQuestionsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseSpecificQuestionsText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CourseSpecificQuestionsphoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseSpecificQuestionsDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CourseSpecificQuestionsUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CourseSpecificQuestionsStates = table.Column<bool>(type: "bit", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    CourseSpecificQuestionsParentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courseSpecificQuestions", x => x.CourseSpecificQuestionsID);
                    table.ForeignKey(
                        name: "FK_courseSpecificQuestions_comments_CourseSpecificQuestionsParentID",
                        column: x => x.CourseSpecificQuestionsParentID,
                        principalTable: "comments",
                        principalColumn: "commentID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_courseSpecificQuestions_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_courseSpecificQuestions_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_courseSpecificQuestions_CourseID",
                table: "courseSpecificQuestions",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_courseSpecificQuestions_CourseSpecificQuestionsParentID",
                table: "courseSpecificQuestions",
                column: "CourseSpecificQuestionsParentID");

            migrationBuilder.CreateIndex(
                name: "IX_courseSpecificQuestions_UserID",
                table: "courseSpecificQuestions",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "courseSpecificQuestions");
        }
    }
}
