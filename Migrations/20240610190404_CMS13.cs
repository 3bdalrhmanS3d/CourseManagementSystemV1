using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace courseManagementSystemV1.Migrations
{
    public partial class CMS13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRejected",
                table: "Users",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "userRejectedDate",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "whoRejectedUser",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "Instructors",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Instructors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Isaccepted",
                table: "courseManagements",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_UserID",
                table: "Instructors",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Users_UserID",
                table: "Instructors",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Users_UserID",
                table: "Instructors");

            migrationBuilder.DropIndex(
                name: "IX_Instructors_UserID",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "IsRejected",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "userRejectedDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "whoRejectedUser",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "Isaccepted",
                table: "courseManagements");
        }
    }
}
