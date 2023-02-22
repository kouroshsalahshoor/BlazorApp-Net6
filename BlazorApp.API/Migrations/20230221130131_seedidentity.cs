using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlazorApp.API.Migrations
{
    /// <inheritdoc />
    public partial class seedidentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "452f15ad-1af8-47a6-8f19-44cebbeeaa88", "926033f6-6ee2-4a36-ad2c-d5e31725d2f5", "Users", "USERS" },
                    { "f14c0cab-9a05-40ed-a87a-9d5b0e468e19", "480f56ca-b571-4bad-953e-958041358d28", "Admins", "ADMINS" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "efc04fbb-f801-4b5c-b7c0-0412723ac404", 0, "30956979-87aa-42a5-8e17-f25590f130d4", "admin@x.x", false, null, null, false, null, null, null, "AQAAAAEAACcQAAAAEKL3nD3/h9l8YmEBsLuI2327D71C/ZL3IMIZ4Jg0V03GmK/cCTlxvStykHUcAGhI8w==", null, false, "a0a75b94-61f2-4ea9-a2eb-a837360ad606", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "f14c0cab-9a05-40ed-a87a-9d5b0e468e19", "efc04fbb-f801-4b5c-b7c0-0412723ac404" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "452f15ad-1af8-47a6-8f19-44cebbeeaa88");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f14c0cab-9a05-40ed-a87a-9d5b0e468e19", "efc04fbb-f801-4b5c-b7c0-0412723ac404" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f14c0cab-9a05-40ed-a87a-9d5b0e468e19");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "efc04fbb-f801-4b5c-b7c0-0412723ac404");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
