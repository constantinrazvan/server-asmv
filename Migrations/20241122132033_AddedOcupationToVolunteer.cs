using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerAsmv.Migrations
{
    /// <inheritdoc />
    public partial class AddedOcupationToVolunteer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ocupation",
                table: "volunteers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2024, 11, 22, 13, 20, 32, 979, DateTimeKind.Utc).AddTicks(946), "$2a$11$CF96no.Nbff5aw2O3d1b1OGwAMi1yZcTaW86nFvAm7//spuPnrJ6i" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2024, 11, 22, 13, 20, 33, 168, DateTimeKind.Utc).AddTicks(1877), "$2a$11$xRRfZ74EkwAx.FcsppjG4.zZiToPLmGeKN6b/BRnUwUY6PW7gnC4u" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ocupation",
                table: "volunteers");

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
    }
}
