using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddNumVillaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NumVilla",
                columns: table => new
                {
                    VillaNo = table.Column<int>(type: "int", nullable: false),
                    Villaid = table.Column<int>(type: "int", nullable: false),
                    EspecialDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumVilla", x => x.VillaNo);
                    table.ForeignKey(
                        name: "FK_NumVilla_Villa_Villaid",
                        column: x => x.Villaid,
                        principalTable: "Villa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Villa",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreate", "DateUpdate" },
                values: new object[] { new DateTime(2023, 2, 22, 14, 0, 26, 837, DateTimeKind.Local).AddTicks(125), new DateTime(2023, 2, 22, 14, 0, 26, 837, DateTimeKind.Local).AddTicks(191) });

            migrationBuilder.UpdateData(
                table: "Villa",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreate", "DateUpdate" },
                values: new object[] { new DateTime(2023, 2, 22, 14, 0, 26, 837, DateTimeKind.Local).AddTicks(196), new DateTime(2023, 2, 22, 14, 0, 26, 837, DateTimeKind.Local).AddTicks(198) });

            migrationBuilder.CreateIndex(
                name: "IX_NumVilla_Villaid",
                table: "NumVilla",
                column: "Villaid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NumVilla");

            migrationBuilder.UpdateData(
                table: "Villa",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreate", "DateUpdate" },
                values: new object[] { new DateTime(2023, 2, 6, 13, 58, 11, 936, DateTimeKind.Local).AddTicks(1227), new DateTime(2023, 2, 6, 13, 58, 11, 936, DateTimeKind.Local).AddTicks(1276) });

            migrationBuilder.UpdateData(
                table: "Villa",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreate", "DateUpdate" },
                values: new object[] { new DateTime(2023, 2, 6, 13, 58, 11, 936, DateTimeKind.Local).AddTicks(1280), new DateTime(2023, 2, 6, 13, 58, 11, 936, DateTimeKind.Local).AddTicks(1282) });
        }
    }
}
