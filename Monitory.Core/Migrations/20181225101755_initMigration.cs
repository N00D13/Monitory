using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Monitory.Core.Migrations
{
    public partial class initMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountID = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountID);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    TaskID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.TaskID);
                });

            migrationBuilder.CreateTable(
                name: "WebCheck",
                columns: table => new
                {
                    WebCheckID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Domain = table.Column<string>(nullable: true),
                    Delay = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    AccountID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebCheck", x => x.WebCheckID);
                    table.ForeignKey(
                        name: "FK_WebCheck_Account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_Username",
                table: "Account",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Task_TaskID",
                table: "Task",
                column: "TaskID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WebCheck_AccountID",
                table: "WebCheck",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_WebCheck_WebCheckID",
                table: "WebCheck",
                column: "WebCheckID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "WebCheck");

            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
