using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ServerAsmv.Migrations
{
    /// <inheritdoc />
    public partial class AddedActivityUserJoinTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Activities_ActivityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ActivityId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ActivityId",
                table: "Users");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Activities",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateTable(
                name: "ActivityUser",
                columns: table => new
                {
                    ActivityId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityUser", x => new { x.ActivityId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ActivityUser_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ActivityUser_UserId",
                table: "ActivityUser",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityUser");

            migrationBuilder.AddColumn<int>(
                name: "ActivityId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Activities",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ActivityId", "CreatedAt", "Password" },
                values: new object[] { null, new DateTime(2024, 11, 3, 23, 8, 35, 175, DateTimeKind.Utc).AddTicks(9622), "$2a$11$cnXnqEgV6bH3hhCsPIqqbOUQb5NeCRDeIqTVmtvRoBihf1x0S.Xri" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ActivityId", "CreatedAt", "Password" },
                values: new object[] { null, new DateTime(2024, 11, 3, 23, 8, 35, 435, DateTimeKind.Utc).AddTicks(6815), "$2a$11$QXFPUUkqdG08y8jImOqg4enRuHGrMBV0/mFycrsQuHPQDr/GpUEXy" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ActivityId",
                table: "Users",
                column: "ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Activities_ActivityId",
                table: "Users",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id");
        }
    }
}
