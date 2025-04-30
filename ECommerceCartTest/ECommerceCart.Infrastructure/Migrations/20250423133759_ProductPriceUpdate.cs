using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerceCart.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProductPriceUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("7bbb5551-72ad-463f-82ae-431dab608612")
            );

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("db577ce3-bbb0-4e44-9636-4e5e5e926ad8")
            );

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Products",
                columns: new[]
                {
                    "ProductId",
                    "CreatedAt",
                    "Description",
                    "Name",
                    "Price",
                    "UpdatedAt",
                },
                values: new object[,]
                {
                    {
                        new Guid("05fbf325-3a9a-4be3-b0dc-9a5178187fbb"),
                        new DateTime(2025, 4, 23, 13, 37, 59, 64, DateTimeKind.Utc).AddTicks(4740),
                        "This is a sample product.",
                        "Sample Product",
                        10.99m,
                        new DateTime(2025, 4, 23, 13, 37, 59, 64, DateTimeKind.Utc).AddTicks(4740),
                    },
                    {
                        new Guid("875540ba-362d-4fa9-b2c1-3cd56420e3f9"),
                        new DateTime(2025, 4, 23, 13, 37, 59, 64, DateTimeKind.Utc).AddTicks(4760),
                        "This is another sample product.",
                        "Another Product",
                        15.99m,
                        new DateTime(2025, 4, 23, 13, 37, 59, 64, DateTimeKind.Utc).AddTicks(4760),
                    },
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("05fbf325-3a9a-4be3-b0dc-9a5178187fbb")
            );

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("875540ba-362d-4fa9-b2c1-3cd56420e3f9")
            );

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Products",
                columns: new[]
                {
                    "ProductId",
                    "CreatedAt",
                    "Description",
                    "Name",
                    "Price",
                    "UpdatedAt",
                },
                values: new object[,]
                {
                    {
                        new Guid("7bbb5551-72ad-463f-82ae-431dab608612"),
                        new DateTime(2025, 4, 23, 13, 35, 20, 479, DateTimeKind.Utc).AddTicks(8540),
                        "This is another sample product.",
                        "Another Product",
                        0m,
                        new DateTime(2025, 4, 23, 13, 35, 20, 479, DateTimeKind.Utc).AddTicks(8540),
                    },
                    {
                        new Guid("db577ce3-bbb0-4e44-9636-4e5e5e926ad8"),
                        new DateTime(2025, 4, 23, 13, 35, 20, 479, DateTimeKind.Utc).AddTicks(8520),
                        "This is a sample product.",
                        "Sample Product",
                        0m,
                        new DateTime(2025, 4, 23, 13, 35, 20, 479, DateTimeKind.Utc).AddTicks(8520),
                    },
                }
            );
        }
    }
}
