using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerAsmv.Migrations
{
    /// <inheritdoc />
    public partial class AddedOneMoreUserSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2024, 11, 3, 17, 51, 4, 274, DateTimeKind.Utc).AddTicks(3092), "$2a$11$Xjsnokof6u0.5xLuvv.rIe08ve36OrMWKx0MadYFxBrMTGqhuXzPq" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Firstname", "Lastname", "Password", "Role" },
                values: new object[] { 2L, new DateTime(2024, 11, 3, 17, 51, 4, 531, DateTimeKind.Utc).AddTicks(6500), "razvanpana20@gmail.com", "Razvan", "Constantin", "$2a$11$Vdmg3PohVnuqLcOx.9XQs./1i6pv4bY9rFm44bPH5bfPYWaLlD0Nq", "Membru Voluntar" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2024, 11, 3, 16, 53, 0, 410, DateTimeKind.Utc).AddTicks(9343), "$2a$11$RQQ5EROj8t9o29aGbDdINuZWnnJ5sJejDA9Ub.xm9w7Afqw2dsfjm" });
        }
    }
}
