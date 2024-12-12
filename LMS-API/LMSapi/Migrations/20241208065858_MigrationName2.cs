using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSapi.Migrations
{
    /// <inheritdoc />
    public partial class MigrationName2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 12, 8, 12, 28, 56, 452, DateTimeKind.Local).AddTicks(2339));

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_BookId",
                table: "Inventories",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Books_BookId",
                table: "Inventories",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Books_BookId",
                table: "Inventories");

            migrationBuilder.DropIndex(
                name: "IX_Inventories_BookId",
                table: "Inventories");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 12, 8, 2, 14, 17, 110, DateTimeKind.Local).AddTicks(8069));
        }
    }
}
