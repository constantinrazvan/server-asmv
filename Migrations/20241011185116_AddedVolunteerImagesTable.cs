using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerAsmv.Migrations
{
    /// <inheritdoc />
    public partial class AddedVolunteerImagesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerImage_Volunteers_VolunteerId",
                table: "VolunteerImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Volunteers",
                table: "Volunteers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VolunteerImage",
                table: "VolunteerImage");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "Ocupation",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Volunteers");

            migrationBuilder.RenameTable(
                name: "Volunteers",
                newName: "volunteers");

            migrationBuilder.RenameTable(
                name: "VolunteerImage",
                newName: "VolunteerImages");

            migrationBuilder.RenameIndex(
                name: "IX_VolunteerImage_VolunteerId",
                table: "VolunteerImages",
                newName: "IX_VolunteerImages_VolunteerId");

            migrationBuilder.AlterColumn<bool>(
                name: "VicePresident",
                table: "volunteers",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "President",
                table: "volunteers",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_volunteers",
                table: "volunteers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VolunteerImages",
                table: "VolunteerImages",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2024, 10, 11, 18, 51, 15, 955, DateTimeKind.Utc).AddTicks(3590), "$2a$11$FxfxxmoxPKdv6ZZFjl1kUORT8esGiE5KPbh9o7jaA.MkVScfxOFoy" });

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerImages_volunteers_VolunteerId",
                table: "VolunteerImages",
                column: "VolunteerId",
                principalTable: "volunteers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerImages_volunteers_VolunteerId",
                table: "VolunteerImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_volunteers",
                table: "volunteers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VolunteerImages",
                table: "VolunteerImages");

            migrationBuilder.RenameTable(
                name: "volunteers",
                newName: "Volunteers");

            migrationBuilder.RenameTable(
                name: "VolunteerImages",
                newName: "VolunteerImage");

            migrationBuilder.RenameIndex(
                name: "IX_VolunteerImages_VolunteerId",
                table: "VolunteerImage",
                newName: "IX_VolunteerImage_VolunteerId");

            migrationBuilder.AlterColumn<bool>(
                name: "VicePresident",
                table: "Volunteers",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "President",
                table: "Volunteers",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Volunteers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Ocupation",
                table: "Volunteers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Volunteers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Volunteers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Volunteers",
                table: "Volunteers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VolunteerImage",
                table: "VolunteerImage",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2024, 10, 11, 12, 40, 48, 599, DateTimeKind.Utc).AddTicks(1865), "$2a$11$GmvFmCSDnLaNNEekOydQneCLctdhjpFeu0thXVM7xZz0YJVsNVgCa" });

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerImage_Volunteers_VolunteerId",
                table: "VolunteerImage",
                column: "VolunteerId",
                principalTable: "Volunteers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
