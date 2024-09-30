using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerAsmv.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserCreatedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2024, 9, 29, 23, 45, 42, 799, DateTimeKind.Utc).AddTicks(8428), "$2a$11$X9wQrEDAEwpNSYS00E5/ge1sfCYckUjh9sCjTeuO0gD.2dHN5lwea" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2024, 9, 29, 12, 11, 36, 801, DateTimeKind.Utc).AddTicks(7199), "$2a$11$cT2s9Rg13x3QLejtSTpZVeg0iRsTn4xw3zliin6RIDbkfSG92Gow." });
        }
    }
}
