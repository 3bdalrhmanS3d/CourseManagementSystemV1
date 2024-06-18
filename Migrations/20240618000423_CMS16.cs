using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace courseManagementSystemV1.Migrations
{
    public partial class CMS16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UserBirthDay",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "updateHistories",
                columns: table => new
                {
                    updateHistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    updateHistoryBy = table.Column<int>(type: "int", nullable: false),
                    updateHistoryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    currentState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    previouState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    curruntRole = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    previousRloe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_updateHistories", x => x.updateHistoryID);
                    table.ForeignKey(
                        name: "FK_updateHistories_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_updateHistories_UserID",
                table: "updateHistories",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "updateHistories");

            migrationBuilder.DropColumn(
                name: "UserBirthDay",
                table: "Users");
        }
    }
}
