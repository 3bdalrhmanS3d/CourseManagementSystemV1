using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace courseManagementSystemV1.Migrations
{
    public partial class CMS5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "courseDescription",
                table: "Courses",
                newName: "CourseDescription");

            migrationBuilder.AddColumn<DateTime>(
                name: "userAcceptedDate",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "userBlockedDate",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "userDeletedDate",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "whoAcceptedUser",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "whoAdminThisUser",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "whoBlockedUser",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "whoDeletedUser",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CourseRequirements",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "CourseTime",
                table: "Courses",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Courses",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userAcceptedDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "userBlockedDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "userDeletedDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "whoAcceptedUser",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "whoAdminThisUser",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "whoBlockedUser",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "whoDeletedUser",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CourseRequirements",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseTime",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "CourseDescription",
                table: "Courses",
                newName: "courseDescription");
        }
    }
}
