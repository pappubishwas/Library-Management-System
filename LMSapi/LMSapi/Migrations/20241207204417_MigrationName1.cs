using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSapi.Migrations
{
    /// <inheritdoc />
    public partial class MigrationName1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "MembershipDate",
                table: "Members",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 12, 8, 2, 14, 17, 110, DateTimeKind.Local).AddTicks(8069));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MembershipDate",
                table: "Members");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 12, 6, 17, 29, 8, 710, DateTimeKind.Local).AddTicks(8822));
        }
    }
}
