using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorApp.API.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e62a9c73-474e-4ed4-9c2c-3b71e3431461",
                column: "ConcurrencyStamp",
                value: "444c987b-a365-43e8-abd2-e669000ef493");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f87e3da7-3d52-4ea3-900d-f4723aaa0bde",
                column: "ConcurrencyStamp",
                value: "b385975c-23d8-4c24-acc8-a05561db2851");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "99551026-e7fd-42f2-9f05-eec450b4fe81",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ffae85b0-abcf-4a0a-bae3-3d8d095d5e38", "AQAAAAEAACcQAAAAEBI04ShcNQhO64h6s68PH0N+q8ESutmwBCwbxYBPDz3R1pyYxesbBYvEmX34NA4wcg==", "be715930-a5fd-425c-a691-b08ee6ae28d9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e62a9c73-474e-4ed4-9c2c-3b71e3431461",
                column: "ConcurrencyStamp",
                value: "ec6e6d6e-5998-4b31-b06a-f0212271927b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f87e3da7-3d52-4ea3-900d-f4723aaa0bde",
                column: "ConcurrencyStamp",
                value: "c053e72d-0057-44ea-b9f7-4da559f7a56b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "99551026-e7fd-42f2-9f05-eec450b4fe81",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7b08a068-a14e-4f0c-af5a-9dafabb8900e", "AQAAAAEAACcQAAAAEI6keoOPWM+JnpG1R4VlcXC/EWY4jvY1IWB6ngl5rHbkZEBT3PzR+yQSKo0PXBPsKg==", "e1812fa4-53db-4071-a226-ed29350c511f" });
        }
    }
}
