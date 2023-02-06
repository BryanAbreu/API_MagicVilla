using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddDataOnTableVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villa",
                columns: new[] { "Id", "DateCreate", "DateUpdate", "Detail", "ImageURL", "Name", "Occupants", "Rate", "amenidad", "squaremeter" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 2, 6, 13, 58, 11, 936, DateTimeKind.Local).AddTicks(1227), new DateTime(2023, 2, 6, 13, 58, 11, 936, DateTimeKind.Local).AddTicks(1276), "Detail of villa...", "", "Villa Real", 10, 10000.0, "", 150 },
                    { 2, new DateTime(2023, 2, 6, 13, 58, 11, 936, DateTimeKind.Local).AddTicks(1280), new DateTime(2023, 2, 6, 13, 58, 11, 936, DateTimeKind.Local).AddTicks(1282), "beautifull 3 pool", "", "Villa premiun", 4, 5240.0, "", 120 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villa",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villa",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
