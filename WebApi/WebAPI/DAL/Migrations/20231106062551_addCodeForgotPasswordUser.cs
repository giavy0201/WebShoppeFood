using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class addCodeForgotPasswordUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21c71463-5a8f-4a35-91af-6a35adc00870");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4cfca213-7611-4c01-b92e-7a75c24d4033");

            migrationBuilder.AddColumn<string>(
                name: "CodeForgotPassword",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "43b753d7-3248-4692-bb33-8cfac7cb52db", "1", "Admin", "Admin" },
                    { "c415db48-b487-4f90-9252-6c6a9bad1a97", "2", "Customer", "Customer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "43b753d7-3248-4692-bb33-8cfac7cb52db");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c415db48-b487-4f90-9252-6c6a9bad1a97");

            migrationBuilder.DropColumn(
                name: "CodeForgotPassword",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "21c71463-5a8f-4a35-91af-6a35adc00870", "1", "Admin", "Admin" },
                    { "4cfca213-7611-4c01-b92e-7a75c24d4033", "2", "Customer", "Customer" }
                });
        }
    }
}
