using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace courseManagementSystemV1.Migrations
{
    public partial class CMS10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "instructorConfPassword",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "instructorEmail",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "instructorFirstName",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "instructorLastName",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "instructorMiddelName",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "instructorPassword",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "instructorPhoneNumber",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "instructorPhoto",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "instructorSSN",
                table: "Instructors");

            migrationBuilder.RenameColumn(
                name: "instructorCreatedAccount",
                table: "Instructors",
                newName: "instructorDateAt");

            migrationBuilder.AddColumn<bool>(
                name: "IsMentor",
                table: "Users",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "userbeMentorDate",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "whoMentorUser",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsInstructor",
                table: "Instructors",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMentor",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "userbeMentorDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "whoMentorUser",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsInstructor",
                table: "Instructors");

            migrationBuilder.RenameColumn(
                name: "instructorDateAt",
                table: "Instructors",
                newName: "instructorCreatedAccount");

            migrationBuilder.AddColumn<string>(
                name: "instructorConfPassword",
                table: "Instructors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "instructorEmail",
                table: "Instructors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "instructorFirstName",
                table: "Instructors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "instructorLastName",
                table: "Instructors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "instructorMiddelName",
                table: "Instructors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "instructorPassword",
                table: "Instructors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "instructorPhoneNumber",
                table: "Instructors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "instructorPhoto",
                table: "Instructors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "instructorSSN",
                table: "Instructors",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: false,
                defaultValue: "");
        }
    }
}
