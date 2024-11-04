using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{   
    /// <inheritdoc />
    public partial class AddCommentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "342cc35b-1707-4e31-b3d6-cc503ac77388");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "916792f8-6cb7-4454-88dd-362259d9a396");

            migrationBuilder.CreateTable(
     name: "Comments",
     columns: table => new
     {
         Id = table.Column<int>(type: "int", nullable: false)
             .Annotation("SqlServer:Identity", "1, 1"),
         Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
         StarRating = table.Column<double>(type: "float", nullable: false),
         CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
         UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
         StoreId = table.Column<int>(type: "int", nullable: false),
         FoodId = table.Column<int>(type: "int", nullable: false),
         AddorUpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
         AddorUpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
         Status = table.Column<int>(type: "int", nullable: true)
     },
     constraints: table =>
     {
         table.PrimaryKey("PK_Comments", x => x.Id);
         table.ForeignKey(
             name: "FK_Comments_Foods_FoodId",
             column: x => x.FoodId,
             principalTable: "Foods",
             principalColumn: "Id",
             onDelete: ReferentialAction.Cascade);
         table.ForeignKey(
             name: "FK_Comments_Stores_StoreId",
             column: x => x.StoreId,
             principalTable: "Stores",
             principalColumn: "StoreID",
             onDelete: ReferentialAction.NoAction); // Hoặc ReferentialAction.SetNull
     });


            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "47280d03-8595-433e-9498-65f6e2e52ccc", "1", "Admin", "Admin" },
                    { "8687ffb3-7ef8-431e-b805-4c7f13104cad", "2", "Customer", "Customer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_FoodId",
                table: "Comments",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_StoreId",
                table: "Comments",
                column: "StoreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "47280d03-8595-433e-9498-65f6e2e52ccc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8687ffb3-7ef8-431e-b805-4c7f13104cad");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "342cc35b-1707-4e31-b3d6-cc503ac77388", "1", "Admin", "Admin" },
                    { "916792f8-6cb7-4454-88dd-362259d9a396", "2", "Customer", "Customer" }
                });
        }
    }
}
