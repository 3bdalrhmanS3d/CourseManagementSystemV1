using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace courseManagementSystemV1.Migrations
{
    public partial class CMS25 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "courseManagements",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_courseManagements_UserID",
                table: "courseManagements",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_courseManagements_Users_UserID",
                table: "courseManagements",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courseManagements_Users_UserID",
                table: "courseManagements");

            migrationBuilder.DropIndex(
                name: "IX_courseManagements_UserID",
                table: "courseManagements");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "courseManagements");
        }
    }
}
