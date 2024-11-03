using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ServerAsmv.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedVolunteerToContainImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VolunteerImage",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Url = table.Column<string>(type: "text", nullable: false),
                    VolunteerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolunteerImage_Volunteers_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Volunteers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2024, 10, 5, 10, 16, 23, 160, DateTimeKind.Utc).AddTicks(9321), "$2a$11$ctW/mno0.0kD3uAi5nM6xesYtuzjN.dkqtblbfQViZ7gv4LdTSKAu" });

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerImage_VolunteerId",
                table: "VolunteerImage",
                column: "VolunteerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VolunteerImage");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2024, 9, 29, 23, 45, 42, 799, DateTimeKind.Utc).AddTicks(8428), "$2a$11$X9wQrEDAEwpNSYS00E5/ge1sfCYckUjh9sCjTeuO0gD.2dHN5lwea" });
        }
    }
}
