using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerAsmv.Migrations
{
    /// <inheritdoc />
    public partial class DeletedDescriptionFromActivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Activities");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2024, 11, 3, 23, 8, 35, 175, DateTimeKind.Utc).AddTicks(9622), "$2a$11$cnXnqEgV6bH3hhCsPIqqbOUQb5NeCRDeIqTVmtvRoBihf1x0S.Xri" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2024, 11, 3, 23, 8, 35, 435, DateTimeKind.Utc).AddTicks(6815), "$2a$11$QXFPUUkqdG08y8jImOqg4enRuHGrMBV0/mFycrsQuHPQDr/GpUEXy" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Activities",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2024, 11, 3, 23, 5, 11, 262, DateTimeKind.Utc).AddTicks(6199), "$2a$11$zKY1rzaNsMhR74x2CV8wXe3FY0C7lxRpKynY4/j50ZaZni2lJ1.Yu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2024, 11, 3, 23, 5, 11, 513, DateTimeKind.Utc).AddTicks(808), "$2a$11$59UzWdMovFxOq9Ij8WS9l.gyeRAgOYMWEA3td5erkFO.e7EFdPMQy" });
        }
    }
}
