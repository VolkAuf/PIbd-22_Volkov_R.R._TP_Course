using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AllDeductedDatabaseImplement.Migrations
{
    public partial class InitCreateUpdStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "date_create",
                table: "studying_status",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "date_create",
                table: "studying_status");
        }
    }
}
