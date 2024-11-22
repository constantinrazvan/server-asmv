using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerAsmv.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewColumnInVolunteersForSecretary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Secretary",
                table: "volunteers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2024, 11, 22, 0, 20, 0, 665, DateTimeKind.Utc).AddTicks(8063), "$2a$11$2Bd2tbP7261YFgtzzAVhU.nf.A.U5.XsXERuDbEGgBsm0/4rhmQaS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2024, 11, 22, 0, 20, 0, 839, DateTimeKind.Utc).AddTicks(7918), "$2a$11$En8LUV06Kovy0fIe1hoTwO2PUIj7KqNBxA9q0dzT3lPSavFX8llXW" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Secretary",
                table: "volunteers");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2024, 11, 14, 21, 7, 38, 865, DateTimeKind.Utc).AddTicks(3121), "$2a$11$.3sPZUfkkmJFYzxmiuanGOauRt.Kr.YWZm6Fnspya6/2ag6d5h0ue" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2024, 11, 14, 21, 7, 39, 55, DateTimeKind.Utc).AddTicks(8193), "$2a$11$sKOqiVVhMDBhsE1oDBCeTOmTLRjzLbIV2KhWhiTUbWz5jMhqpl7sS" });
        }
    }
}
