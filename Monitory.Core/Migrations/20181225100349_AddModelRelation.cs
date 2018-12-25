using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Monitory.Core.Migrations
{
    public partial class AddModelRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AccountID",
                table: "WebCheck",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WebCheck_AccountID",
                table: "WebCheck",
                column: "AccountID");

            migrationBuilder.AddForeignKey(
                name: "FK_WebCheck_Account_AccountID",
                table: "WebCheck",
                column: "AccountID",
                principalTable: "Account",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WebCheck_Account_AccountID",
                table: "WebCheck");

            migrationBuilder.DropIndex(
                name: "IX_WebCheck_AccountID",
                table: "WebCheck");

            migrationBuilder.DropColumn(
                name: "AccountID",
                table: "WebCheck");
        }
    }
}
