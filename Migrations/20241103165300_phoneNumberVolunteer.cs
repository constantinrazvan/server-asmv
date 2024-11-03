using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerAsmv.Migrations
{
    /// <inheritdoc />
    public partial class phoneNumberVolunteer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "volunteers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2024, 11, 3, 16, 53, 0, 410, DateTimeKind.Utc).AddTicks(9343), "$2a$11$RQQ5EROj8t9o29aGbDdINuZWnnJ5sJejDA9Ub.xm9w7Afqw2dsfjm" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "volunteers");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2024, 11, 3, 16, 44, 30, 316, DateTimeKind.Utc).AddTicks(9121), "$2a$11$Xr9JjGkZWil1L9Dp/kIwsuEg.cA1oBjnf0CKN4.hC/wEPXlHOaCOa" });
        }
    }
}
