using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerAsmv.Migrations
{
    /// <inheritdoc />
    public partial class changedTypeToDepartmentInVolunteer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "volunteers",
                newName: "Department");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2024, 11, 3, 16, 44, 30, 316, DateTimeKind.Utc).AddTicks(9121), "$2a$11$Xr9JjGkZWil1L9Dp/kIwsuEg.cA1oBjnf0CKN4.hC/wEPXlHOaCOa" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Department",
                table: "volunteers",
                newName: "Type");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2024, 11, 2, 19, 55, 4, 919, DateTimeKind.Utc).AddTicks(8672), "$2a$11$9dtdZzsWelFteNZIENfMf.gwvNJ8IAchczhF9hbuPyH2wm9l84pp." });
        }
    }
}
