using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace courseManagementSystemV1.Migrations
{
    public partial class CMS1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    courseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    courseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    courseStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    courseEndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.courseID);
                });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    instructorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    instructorSSN = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    instructorFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    instructorMiddelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    instructorLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    instructorEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    instructorPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    instructorPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    instructorConfPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    instructorPhoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LindenInAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GitHibAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    instructorCreatedAccount = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsBlocked = table.Column<bool>(type: "bit", nullable: true),
                    ISDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.instructorID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userSSN = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    userFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userMiddelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userPhoneNumber = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    userPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userConfPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userEnterCollegeDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    userCollege = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userDepartment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userUniversity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userPhoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userAddressGov = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userstreet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userCreatedAccount = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsBlocked = table.Column<bool>(type: "bit", nullable: true),
                    ISDeleted = table.Column<bool>(type: "bit", nullable: true),
                    normalUser = table.Column<bool>(type: "bit", nullable: false),
                    IsUserHR = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userID);
                });

            migrationBuilder.CreateTable(
                name: "courseManagements",
                columns: table => new
                {
                    courseMangID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    courseID = table.Column<int>(type: "int", nullable: false),
                    instructorID = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courseManagements", x => x.courseMangID);
                    table.ForeignKey(
                        name: "FK_courseManagements_Cases_courseID",
                        column: x => x.courseID,
                        principalTable: "Cases",
                        principalColumn: "courseID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_courseManagements_Instructors_instructorID",
                        column: x => x.instructorID,
                        principalTable: "Instructors",
                        principalColumn: "instructorID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    enrollmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    enrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    userID = table.Column<int>(type: "int", nullable: false),
                    courseID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.enrollmentID);
                    table.ForeignKey(
                        name: "FK_Enrollments_Cases_courseID",
                        column: x => x.courseID,
                        principalTable: "Cases",
                        principalColumn: "courseID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Enrollments_Users_userID",
                        column: x => x.userID,
                        principalTable: "Users",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_courseManagements_courseID",
                table: "courseManagements",
                column: "courseID");

            migrationBuilder.CreateIndex(
                name: "IX_courseManagements_instructorID",
                table: "courseManagements",
                column: "instructorID");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_courseID",
                table: "Enrollments",
                column: "courseID");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_userID",
                table: "Enrollments",
                column: "userID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "courseManagements");

            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
