using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerAsmv.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Firstname", "Lastname", "Password", "Role" },
                values: new object[] { 1L, new DateTime(2024, 9, 29, 12, 11, 36, 801, DateTimeKind.Utc).AddTicks(7199), "admin@asmv.com", "Admin", "Admin", "$2a$11$cT2s9Rg13x3QLejtSTpZVeg0iRsTn4xw3zliin6RIDbkfSG92Gow.", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
