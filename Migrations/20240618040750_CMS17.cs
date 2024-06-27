using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace courseManagementSystemV1.Migrations
{
    public partial class CMS17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "previousRloe",
                table: "updateHistories");

            migrationBuilder.AlterColumn<string>(
                name: "FeedbackText",
                table: "feedbacks",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AddColumn<int>(
                name: "WhopublushedQuestions",
                table: "courseSpecificQuestions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "quizzes",
                columns: table => new
                {
                    QuizID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HostID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MetaTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalScore = table.Column<int>(type: "int", nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PublishedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartsAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndsAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quizzes", x => x.QuizID);
                    table.ForeignKey(
                        name: "FK_quizzes_Users_HostID",
                        column: x => x.HostID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "grades",
                columns: table => new
                {
                    GradeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    QuizID = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grades", x => x.GradeID);
                    table.ForeignKey(
                        name: "FK_grades_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_grades_quizzes_QuizID",
                        column: x => x.QuizID,
                        principalTable: "quizzes",
                        principalColumn: "QuizID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_grades_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "quizMeta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuizId = table.Column<int>(type: "int", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quizMeta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_quizMeta_quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "quizzes",
                        principalColumn: "QuizID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "quizQuestions",
                columns: table => new
                {
                    QuestionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuizID = table.Column<int>(type: "int", nullable: false),
                    questionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Questionphoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAxtive = table.Column<bool>(type: "bit", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quizQuestions", x => x.QuestionID);
                    table.ForeignKey(
                        name: "FK_quizQuestions_quizzes_QuizID",
                        column: x => x.QuizID,
                        principalTable: "quizzes",
                        principalColumn: "QuizID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "takes",
                columns: table => new
                {
                    TakeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    QuizID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinishedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_takes", x => x.TakeID);
                    table.ForeignKey(
                        name: "FK_takes_quizzes_QuizID",
                        column: x => x.QuizID,
                        principalTable: "quizzes",
                        principalColumn: "QuizID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_takes_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "quizAnswers",
                columns: table => new
                {
                    AnswerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionID = table.Column<int>(type: "int", nullable: false),
                    AnswerContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quizAnswers", x => x.AnswerID);
                    table.ForeignKey(
                        name: "FK_quizAnswers_quizQuestions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "quizQuestions",
                        principalColumn: "QuestionID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "takesAnswer",
                columns: table => new
                {
                    TakeAnswerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TakeID = table.Column<int>(type: "int", nullable: false),
                    AnswerID = table.Column<int>(type: "int", nullable: false),
                    QuestionID = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_takesAnswer", x => x.TakeAnswerID);
                    table.ForeignKey(
                        name: "FK_takesAnswer_quizAnswers_AnswerID",
                        column: x => x.AnswerID,
                        principalTable: "quizAnswers",
                        principalColumn: "AnswerID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_takesAnswer_quizQuestions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "quizQuestions",
                        principalColumn: "QuestionID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_takesAnswer_takes_TakeID",
                        column: x => x.TakeID,
                        principalTable: "takes",
                        principalColumn: "TakeID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_grades_CourseID",
                table: "grades",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_grades_QuizID",
                table: "grades",
                column: "QuizID");

            migrationBuilder.CreateIndex(
                name: "IX_grades_UserID",
                table: "grades",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_quizAnswers_QuestionID",
                table: "quizAnswers",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_quizMeta_QuizId",
                table: "quizMeta",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_quizQuestions_QuizID",
                table: "quizQuestions",
                column: "QuizID");

            migrationBuilder.CreateIndex(
                name: "IX_quizzes_HostID",
                table: "quizzes",
                column: "HostID");

            migrationBuilder.CreateIndex(
                name: "IX_takes_QuizID",
                table: "takes",
                column: "QuizID");

            migrationBuilder.CreateIndex(
                name: "IX_takes_UserID",
                table: "takes",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_takesAnswer_AnswerID",
                table: "takesAnswer",
                column: "AnswerID");

            migrationBuilder.CreateIndex(
                name: "IX_takesAnswer_QuestionID",
                table: "takesAnswer",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_takesAnswer_TakeID",
                table: "takesAnswer",
                column: "TakeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "grades");

            migrationBuilder.DropTable(
                name: "quizMeta");

            migrationBuilder.DropTable(
                name: "takesAnswer");

            migrationBuilder.DropTable(
                name: "quizAnswers");

            migrationBuilder.DropTable(
                name: "takes");

            migrationBuilder.DropTable(
                name: "quizQuestions");

            migrationBuilder.DropTable(
                name: "quizzes");

            migrationBuilder.DropColumn(
                name: "WhopublushedQuestions",
                table: "courseSpecificQuestions");

            migrationBuilder.AddColumn<string>(
                name: "previousRloe",
                table: "updateHistories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FeedbackText",
                table: "feedbacks",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);
        }
    }
}
