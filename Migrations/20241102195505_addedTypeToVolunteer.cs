using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerAsmv.Migrations
{
    /// <inheritdoc />
    public partial class addedTypeToVolunteer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "volunteers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2024, 11, 2, 19, 55, 4, 919, DateTimeKind.Utc).AddTicks(8672), "$2a$11$9dtdZzsWelFteNZIENfMf.gwvNJ8IAchczhF9hbuPyH2wm9l84pp." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "volunteers");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2024, 10, 11, 18, 51, 15, 955, DateTimeKind.Utc).AddTicks(3590), "$2a$11$FxfxxmoxPKdv6ZZFjl1kUORT8esGiE5KPbh9o7jaA.MkVScfxOFoy" });
        }
    }
}
