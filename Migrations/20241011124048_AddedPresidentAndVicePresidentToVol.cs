using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerAsmv.Migrations
{
    /// <inheritdoc />
    public partial class AddedPresidentAndVicePresidentToVol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "President",
                table: "Volunteers",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "VicePresident",
                table: "Volunteers",
                type: "boolean",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2024, 10, 11, 12, 40, 48, 599, DateTimeKind.Utc).AddTicks(1865), "$2a$11$GmvFmCSDnLaNNEekOydQneCLctdhjpFeu0thXVM7xZz0YJVsNVgCa" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "President",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "VicePresident",
                table: "Volunteers");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2024, 10, 10, 23, 52, 14, 774, DateTimeKind.Utc).AddTicks(4336), "$2a$11$K0m5biXQqqDiPLpk93CGpu0OVX/IKb5melxEw/mk5je9W90FCGavW" });
        }
    }
}
