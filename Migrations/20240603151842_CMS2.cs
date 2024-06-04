using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace courseManagementSystemV1.Migrations
{
    public partial class CMS2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courseManagements_Cases_courseID",
                table: "courseManagements");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Cases_courseID",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Users_userID",
                table: "Enrollments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cases",
                table: "Cases");

            migrationBuilder.RenameTable(
                name: "Cases",
                newName: "Courses");

            migrationBuilder.RenameColumn(
                name: "userstreet",
                table: "Users",
                newName: "UserStreet");

            migrationBuilder.RenameColumn(
                name: "userUniversity",
                table: "Users",
                newName: "UserUniversity");

            migrationBuilder.RenameColumn(
                name: "userSSN",
                table: "Users",
                newName: "UserSSN");

            migrationBuilder.RenameColumn(
                name: "userPhoto",
                table: "Users",
                newName: "UserPhoto");

            migrationBuilder.RenameColumn(
                name: "userPhoneNumber",
                table: "Users",
                newName: "UserPhoneNumber");

            migrationBuilder.RenameColumn(
                name: "userPassword",
                table: "Users",
                newName: "UserPassword");

            migrationBuilder.RenameColumn(
                name: "userMiddelName",
                table: "Users",
                newName: "UserMiddelName");

            migrationBuilder.RenameColumn(
                name: "userLastName",
                table: "Users",
                newName: "UserLastName");

            migrationBuilder.RenameColumn(
                name: "userFirstName",
                table: "Users",
                newName: "UserFirstName");

            migrationBuilder.RenameColumn(
                name: "userEnterCollegeDateTime",
                table: "Users",
                newName: "UserEnterCollegeDateTime");

            migrationBuilder.RenameColumn(
                name: "userEmail",
                table: "Users",
                newName: "UserEmail");

            migrationBuilder.RenameColumn(
                name: "userDepartment",
                table: "Users",
                newName: "UserDepartment");

            migrationBuilder.RenameColumn(
                name: "userCreatedAccount",
                table: "Users",
                newName: "UserCreatedAccount");

            migrationBuilder.RenameColumn(
                name: "userConfPassword",
                table: "Users",
                newName: "UserConfPassword");

            migrationBuilder.RenameColumn(
                name: "userCollege",
                table: "Users",
                newName: "UserCollege");

            migrationBuilder.RenameColumn(
                name: "userCity",
                table: "Users",
                newName: "UserCity");

            migrationBuilder.RenameColumn(
                name: "userAddressGov",
                table: "Users",
                newName: "UserAddressGov");

            migrationBuilder.RenameColumn(
                name: "normalUser",
                table: "Users",
                newName: "NormalUser");

            migrationBuilder.RenameColumn(
                name: "ISDeleted",
                table: "Users",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "userID",
                table: "Users",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "userID",
                table: "Enrollments",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "enrollmentDate",
                table: "Enrollments",
                newName: "EnrollmentDate");

            migrationBuilder.RenameColumn(
                name: "courseID",
                table: "Enrollments",
                newName: "CourseID");

            migrationBuilder.RenameColumn(
                name: "enrollmentID",
                table: "Enrollments",
                newName: "EnrollmentID");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_userID",
                table: "Enrollments",
                newName: "IX_Enrollments_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_courseID",
                table: "Enrollments",
                newName: "IX_Enrollments_CourseID");

            migrationBuilder.RenameColumn(
                name: "courseStartDate",
                table: "Courses",
                newName: "CourseStartDate");

            migrationBuilder.RenameColumn(
                name: "courseName",
                table: "Courses",
                newName: "CourseName");

            migrationBuilder.RenameColumn(
                name: "courseEndDate",
                table: "Courses",
                newName: "CourseEndDate");

            migrationBuilder.RenameColumn(
                name: "courseID",
                table: "Courses",
                newName: "CourseID");

            migrationBuilder.AddColumn<string>(
                name: "CoursePhoto",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                table: "Courses",
                column: "CourseID");

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    AttendanceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnrollmentID = table.Column<int>(type: "int", nullable: false),
                    SessionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckInTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CheckOutTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    BreakStartTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    BreakEndTime = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.AttendanceID);
                    table.ForeignKey(
                        name: "FK_Attendances_Enrollments_EnrollmentID",
                        column: x => x.EnrollmentID,
                        principalTable: "Enrollments",
                        principalColumn: "EnrollmentID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Bonus",
                columns: table => new
                {
                    BonusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    BonusType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BonusAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateAwarded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bonus", x => x.BonusID);
                    table.ForeignKey(
                        name: "FK_Bonus_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Bonus_Users_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.NoAction    );
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_EnrollmentID",
                table: "Attendances",
                column: "EnrollmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Bonus_CourseID",
                table: "Bonus",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Bonus_StudentID",
                table: "Bonus",
                column: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_courseManagements_Courses_courseID",
                table: "courseManagements",
                column: "courseID",
                principalTable: "Courses",
                principalColumn: "CourseID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Courses_CourseID",
                table: "Enrollments",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "CourseID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Users_UserID",
                table: "Enrollments",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courseManagements_Courses_courseID",
                table: "courseManagements");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Courses_CourseID",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Users_UserID",
                table: "Enrollments");

            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "Bonus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CoursePhoto",
                table: "Courses");

            migrationBuilder.RenameTable(
                name: "Courses",
                newName: "Cases");

            migrationBuilder.RenameColumn(
                name: "UserUniversity",
                table: "Users",
                newName: "userUniversity");

            migrationBuilder.RenameColumn(
                name: "UserStreet",
                table: "Users",
                newName: "userstreet");

            migrationBuilder.RenameColumn(
                name: "UserSSN",
                table: "Users",
                newName: "userSSN");

            migrationBuilder.RenameColumn(
                name: "UserPhoto",
                table: "Users",
                newName: "userPhoto");

            migrationBuilder.RenameColumn(
                name: "UserPhoneNumber",
                table: "Users",
                newName: "userPhoneNumber");

            migrationBuilder.RenameColumn(
                name: "UserPassword",
                table: "Users",
                newName: "userPassword");

            migrationBuilder.RenameColumn(
                name: "UserMiddelName",
                table: "Users",
                newName: "userMiddelName");

            migrationBuilder.RenameColumn(
                name: "UserLastName",
                table: "Users",
                newName: "userLastName");

            migrationBuilder.RenameColumn(
                name: "UserFirstName",
                table: "Users",
                newName: "userFirstName");

            migrationBuilder.RenameColumn(
                name: "UserEnterCollegeDateTime",
                table: "Users",
                newName: "userEnterCollegeDateTime");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "Users",
                newName: "userEmail");

            migrationBuilder.RenameColumn(
                name: "UserDepartment",
                table: "Users",
                newName: "userDepartment");

            migrationBuilder.RenameColumn(
                name: "UserCreatedAccount",
                table: "Users",
                newName: "userCreatedAccount");

            migrationBuilder.RenameColumn(
                name: "UserConfPassword",
                table: "Users",
                newName: "userConfPassword");

            migrationBuilder.RenameColumn(
                name: "UserCollege",
                table: "Users",
                newName: "userCollege");

            migrationBuilder.RenameColumn(
                name: "UserCity",
                table: "Users",
                newName: "userCity");

            migrationBuilder.RenameColumn(
                name: "UserAddressGov",
                table: "Users",
                newName: "userAddressGov");

            migrationBuilder.RenameColumn(
                name: "NormalUser",
                table: "Users",
                newName: "normalUser");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Users",
                newName: "ISDeleted");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Users",
                newName: "userID");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Enrollments",
                newName: "userID");

            migrationBuilder.RenameColumn(
                name: "EnrollmentDate",
                table: "Enrollments",
                newName: "enrollmentDate");

            migrationBuilder.RenameColumn(
                name: "CourseID",
                table: "Enrollments",
                newName: "courseID");

            migrationBuilder.RenameColumn(
                name: "EnrollmentID",
                table: "Enrollments",
                newName: "enrollmentID");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_UserID",
                table: "Enrollments",
                newName: "IX_Enrollments_userID");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_CourseID",
                table: "Enrollments",
                newName: "IX_Enrollments_courseID");

            migrationBuilder.RenameColumn(
                name: "CourseStartDate",
                table: "Cases",
                newName: "courseStartDate");

            migrationBuilder.RenameColumn(
                name: "CourseName",
                table: "Cases",
                newName: "courseName");

            migrationBuilder.RenameColumn(
                name: "CourseEndDate",
                table: "Cases",
                newName: "courseEndDate");

            migrationBuilder.RenameColumn(
                name: "CourseID",
                table: "Cases",
                newName: "courseID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cases",
                table: "Cases",
                column: "courseID");

            migrationBuilder.AddForeignKey(
                name: "FK_courseManagements_Cases_courseID",
                table: "courseManagements",
                column: "courseID",
                principalTable: "Cases",
                principalColumn: "courseID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Cases_courseID",
                table: "Enrollments",
                column: "courseID",
                principalTable: "Cases",
                principalColumn: "courseID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Users_userID",
                table: "Enrollments",
                column: "userID",
                principalTable: "Users",
                principalColumn: "userID",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
