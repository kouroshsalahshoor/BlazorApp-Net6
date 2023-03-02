using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorApp.API.Migrations
{
    /// <inheritdoc />
    public partial class authoridinbook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Books",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e62a9c73-474e-4ed4-9c2c-3b71e3431461",
                column: "ConcurrencyStamp",
                value: "8fad4e16-0ed6-4096-89ee-580ab7036ce4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f87e3da7-3d52-4ea3-900d-f4723aaa0bde",
                column: "ConcurrencyStamp",
                value: "9bdd22ff-00d8-438d-8f01-6072daa39f29");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "99551026-e7fd-42f2-9f05-eec450b4fe81",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6a89204e-f583-4d73-bb6d-b36fe1a5ef9d", "AQAAAAEAACcQAAAAEBOwmpo5mTbsQfPQFofXftwQ2MtSKZQ78pGjeqi6hwLLtJfwfNqqXVa7dy4xa1YF9g==", "b46b5d47-51d3-4c50-8fab-51d37cbbf508" });

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e62a9c73-474e-4ed4-9c2c-3b71e3431461",
                column: "ConcurrencyStamp",
                value: "4dd8a528-7d9d-49f5-bc6b-a5794078fc12");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f87e3da7-3d52-4ea3-900d-f4723aaa0bde",
                column: "ConcurrencyStamp",
                value: "1e1f8ba8-6f2a-482f-a99c-494dafac6904");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "99551026-e7fd-42f2-9f05-eec450b4fe81",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c7b03f2d-e302-4e85-825a-570300fa9b3a", "AQAAAAEAACcQAAAAEMdvXLwPjQcPJW5BWTBkXDFYcpfIzRB63UzUDBOua8BL0coIwg9C8RnW6hPLMOF9wQ==", "c6760f54-fa85-4be4-a256-ad6e031c25a7" });

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
