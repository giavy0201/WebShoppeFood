using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class addAccountStore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "477beb2c-684e-4f0a-a340-98ff809c8a44");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "90c16a8a-8295-4b8e-8b94-53c879818200");

            migrationBuilder.CreateTable(
                name: "AccountStores",
                columns: table => new
                {
                    AccountStoreID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoginName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StoreID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountStores", x => x.AccountStoreID);
                    table.ForeignKey(
                        name: "FK_AccountStores_Stores_StoreID",
                        column: x => x.StoreID,
                        principalTable: "Stores",
                        principalColumn: "StoreID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9f05b71f-8716-41d4-bd26-4b00dcb76dee", "2", "Customer", "Customer" },
                    { "c2a11630-e3a8-4aaf-a930-332ff5c7fe41", "1", "Admin", "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountStores_StoreID",
                table: "AccountStores",
                column: "StoreID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountStores");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9f05b71f-8716-41d4-bd26-4b00dcb76dee");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c2a11630-e3a8-4aaf-a930-332ff5c7fe41");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "477beb2c-684e-4f0a-a340-98ff809c8a44", "1", "Admin", "Admin" },
                    { "90c16a8a-8295-4b8e-8b94-53c879818200", "2", "Customer", "Customer" }
                });
        }
    }
}
