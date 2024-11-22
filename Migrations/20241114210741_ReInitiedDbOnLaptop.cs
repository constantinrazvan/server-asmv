using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerAsmv.Migrations
{
    /// <inheritdoc />
    public partial class ReInitiedDbOnLaptop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2024, 11, 3, 23, 23, 9, 607, DateTimeKind.Utc).AddTicks(8832), "$2a$11$FBnUTdvRMn6RKzce7xYskuKIx3uBTH4mSzXvszeBuZH6oByolCJy." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2024, 11, 3, 23, 23, 9, 896, DateTimeKind.Utc).AddTicks(9108), "$2a$11$iOrfIQZ6fVobC4MidTl0T.0feK.yoe5QgxF3BMOYoZ2o2OC/Fa2d6" });
        }
    }
}
