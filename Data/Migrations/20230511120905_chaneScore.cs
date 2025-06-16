using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Highscore.Data.Migrations
{
    /// <inheritdoc />
    public partial class chaneScore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "649ea063-203b-48b9-a2bd-38fe7e9b18c3");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "602031f7-289f-451b-972c-38c3ade3f1e0", null, "Administrator", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "602031f7-289f-451b-972c-38c3ade3f1e0");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "649ea063-203b-48b9-a2bd-38fe7e9b18c3", null, "Administrator", null });
        }
    }
}
