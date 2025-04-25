using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerceCart.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedProductPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("928aaf70-ba7f-4eab-9f29-0c4fa3143e13"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("c2a6e15d-a3fc-403e-b019-af51f8b6b941"));

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                schema: "dbo",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Products",
                columns: new[] { "ProductId", "CreatedAt", "Description", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("7bbb5551-72ad-463f-82ae-431dab608612"), new DateTime(2025, 4, 23, 13, 35, 20, 479, DateTimeKind.Utc).AddTicks(8540), "This is another sample product.", "Another Product", 0m, new DateTime(2025, 4, 23, 13, 35, 20, 479, DateTimeKind.Utc).AddTicks(8540) },
                    { new Guid("db577ce3-bbb0-4e44-9636-4e5e5e926ad8"), new DateTime(2025, 4, 23, 13, 35, 20, 479, DateTimeKind.Utc).AddTicks(8520), "This is a sample product.", "Sample Product", 0m, new DateTime(2025, 4, 23, 13, 35, 20, 479, DateTimeKind.Utc).AddTicks(8520) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("7bbb5551-72ad-463f-82ae-431dab608612"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("db577ce3-bbb0-4e44-9636-4e5e5e926ad8"));

            migrationBuilder.DropColumn(
                name: "Price",
                schema: "dbo",
                table: "Products");

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Products",
                columns: new[] { "ProductId", "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("928aaf70-ba7f-4eab-9f29-0c4fa3143e13"), new DateTime(2025, 4, 23, 9, 55, 42, 762, DateTimeKind.Utc).AddTicks(2350), "This is a sample product.", "Sample Product", new DateTime(2025, 4, 23, 9, 55, 42, 762, DateTimeKind.Utc).AddTicks(2350) },
                    { new Guid("c2a6e15d-a3fc-403e-b019-af51f8b6b941"), new DateTime(2025, 4, 23, 9, 55, 42, 762, DateTimeKind.Utc).AddTicks(2370), "This is another sample product.", "Another Product", new DateTime(2025, 4, 23, 9, 55, 42, 762, DateTimeKind.Utc).AddTicks(2370) }
                });
        }
    }
}
