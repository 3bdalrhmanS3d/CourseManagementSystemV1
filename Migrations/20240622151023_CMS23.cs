using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace courseManagementSystemV1.Migrations
{
    public partial class CMS23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courseSpecificQuestions_comments_CourseSpecificQuestionsParentID",
                table: "courseSpecificQuestions");

            migrationBuilder.AlterColumn<int>(
                name: "CourseSpecificQuestionsParentID",
                table: "courseSpecificQuestions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_courseSpecificQuestions_comments_CourseSpecificQuestionsParentID",
                table: "courseSpecificQuestions",
                column: "CourseSpecificQuestionsParentID",
                principalTable: "comments",
                principalColumn: "commentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courseSpecificQuestions_comments_CourseSpecificQuestionsParentID",
                table: "courseSpecificQuestions");

            migrationBuilder.AlterColumn<int>(
                name: "CourseSpecificQuestionsParentID",
                table: "courseSpecificQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_courseSpecificQuestions_comments_CourseSpecificQuestionsParentID",
                table: "courseSpecificQuestions",
                column: "CourseSpecificQuestionsParentID",
                principalTable: "comments",
                principalColumn: "commentID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
