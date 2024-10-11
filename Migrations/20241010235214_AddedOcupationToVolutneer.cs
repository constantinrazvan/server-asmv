using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerAsmv.Migrations
{
    /// <inheritdoc />
    public partial class AddedOcupationToVolutneer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ocupation",
                table: "Volunteers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2024, 10, 10, 23, 52, 14, 774, DateTimeKind.Utc).AddTicks(4336), "$2a$11$K0m5biXQqqDiPLpk93CGpu0OVX/IKb5melxEw/mk5je9W90FCGavW" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ocupation",
                table: "Volunteers");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2024, 10, 5, 10, 16, 23, 160, DateTimeKind.Utc).AddTicks(9321), "$2a$11$ctW/mno0.0kD3uAi5nM6xesYtuzjN.dkqtblbfQViZ7gv4LdTSKAu" });
        }
    }
}
