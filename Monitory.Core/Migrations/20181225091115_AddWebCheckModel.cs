using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Monitory.Core.Migrations
{
    public partial class AddWebCheckModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WebCheck",
                columns: table => new
                {
                    WebCheckID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Domain = table.Column<string>(nullable: true),
                    Delay = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebCheck", x => x.WebCheckID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WebCheck_WebCheckID",
                table: "WebCheck",
                column: "WebCheckID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebCheck");
        }
    }
}
