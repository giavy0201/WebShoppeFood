using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class addColumnNameNoDiacritic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "39d1b2d5-39fc-4ae7-8093-5e3466dfaccb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d5ae2b5b-3930-43c9-a3bb-94b8a995812d");

            migrationBuilder.AddColumn<string>(
                name: "AddressNoDiacritic",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameNoDiacritic",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameNoDiacritic",
                table: "Foods",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryNoDiacritic",
                table: "Carts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9c06ecca-70f8-415f-865d-aacae076ecb2", "2", "Customer", "Customer" },
                    { "b740904d-6bcf-4522-b6d6-cb42e705ea04", "1", "Admin", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c06ecca-70f8-415f-865d-aacae076ecb2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b740904d-6bcf-4522-b6d6-cb42e705ea04");

            migrationBuilder.DropColumn(
                name: "AddressNoDiacritic",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "NameNoDiacritic",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "NameNoDiacritic",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "DeliveryNoDiacritic",
                table: "Carts");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "39d1b2d5-39fc-4ae7-8093-5e3466dfaccb", "1", "Admin", "Admin" },
                    { "d5ae2b5b-3930-43c9-a3bb-94b8a995812d", "2", "Customer", "Customer" }
                });
        }
    }
}
