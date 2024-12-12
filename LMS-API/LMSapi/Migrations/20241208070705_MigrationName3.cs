using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LMSapi.Migrations
{
    /// <inheritdoc />
    public partial class MigrationName3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Inventories",
                columns: new[] { "InventoryId", "BookId", "Condition", "Location", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, "Good", "Main Library - Shelf A1", 5 },
                    { 2, 2, "Good", "Main Library - Shelf A1", 3 },
                    { 3, 3, "Damaged", "Main Library - Shelf A2", 4 },
                    { 4, 4, "Good", "Main Library - Shelf A3", 6 },
                    { 5, 5, "Good", "Programming Section - Shelf B1", 7 },
                    { 6, 6, "Good", "Programming Section - Shelf B2", 5 },
                    { 7, 7, "Damaged", "Networking Section - Shelf C1", 2 },
                    { 8, 8, "Lost", "Hardware Section - Shelf D1", 3 },
                    { 9, 9, "Good", "Mechanical Section - Shelf E1", 4 },
                    { 10, 10, "Good", "Mechanical Section - Shelf E2", 6 },
                    { 11, 11, "Damaged", "Mathematics Section - Shelf F1", 2 },
                    { 12, 12, "Good", "Mathematics Section - Shelf F2", 10 },
                    { 13, 13, "Good", "Main Library - Shelf A4", 3 },
                    { 14, 14, "Good", "Programming Section - Shelf B3", 4 },
                    { 15, 15, "Good", "Networking Section - Shelf C2", 5 },
                    { 16, 16, "Good", "Hardware Section - Shelf D2", 6 },
                    { 17, 17, "Good", "Mechanical Section - Shelf E3", 3 },
                    { 18, 18, "Damaged", "Mechanical Section - Shelf E4", 2 },
                    { 19, 19, "Good", "Mathematics Section - Shelf F3", 4 },
                    { 20, 20, "Good", "Science Section - Shelf G1", 7 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 12, 8, 12, 37, 5, 410, DateTimeKind.Local).AddTicks(2583));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "InventoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "InventoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "InventoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "InventoryId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "InventoryId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "InventoryId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "InventoryId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "InventoryId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "InventoryId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "InventoryId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "InventoryId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "InventoryId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "InventoryId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "InventoryId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "InventoryId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "InventoryId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "InventoryId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "InventoryId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "InventoryId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "InventoryId",
                keyValue: 20);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 12, 8, 12, 28, 56, 452, DateTimeKind.Local).AddTicks(2339));
        }
    }
}
