using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace courseManagementSystemV1.Migrations
{
    public partial class CMS4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "Users",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "courseDescription",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "HRManagements",
                columns: table => new
                {
                    HRManagementID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRManagements", x => x.HRManagementID);
                    table.ForeignKey(
                        name: "FK_HRManagements_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_HRManagements_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HRManagements_CourseID",
                table: "HRManagements",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_HRManagements_UserID",
                table: "HRManagements",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HRManagements");

            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "courseDescription",
                table: "Courses");
        }
    }
}
