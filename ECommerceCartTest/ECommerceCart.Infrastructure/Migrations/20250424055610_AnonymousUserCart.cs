using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerceCart.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AnonymousUserCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("05fbf325-3a9a-4be3-b0dc-9a5178187fbb"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("875540ba-362d-4fa9-b2c1-3cd56420e3f9"));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                schema: "dbo",
                table: "Carts",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Products",
                columns: new[] { "ProductId", "CreatedAt", "Description", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("a16f14b8-88b2-4434-8c41-bf13935bee1c"), new DateTime(2025, 4, 24, 5, 56, 10, 425, DateTimeKind.Utc).AddTicks(9340), "This is a sample product.", "Sample Product", 10.99m, new DateTime(2025, 4, 24, 5, 56, 10, 425, DateTimeKind.Utc).AddTicks(9340) },
                    { new Guid("f910661a-36cc-4c6e-a0d3-0ee992cadc74"), new DateTime(2025, 4, 24, 5, 56, 10, 425, DateTimeKind.Utc).AddTicks(9370), "This is another sample product.", "Another Product", 15.99m, new DateTime(2025, 4, 24, 5, 56, 10, 425, DateTimeKind.Utc).AddTicks(9370) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("a16f14b8-88b2-4434-8c41-bf13935bee1c"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("f910661a-36cc-4c6e-a0d3-0ee992cadc74"));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                schema: "dbo",
                table: "Carts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Products",
                columns: new[] { "ProductId", "CreatedAt", "Description", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("05fbf325-3a9a-4be3-b0dc-9a5178187fbb"), new DateTime(2025, 4, 23, 13, 37, 59, 64, DateTimeKind.Utc).AddTicks(4740), "This is a sample product.", "Sample Product", 10.99m, new DateTime(2025, 4, 23, 13, 37, 59, 64, DateTimeKind.Utc).AddTicks(4740) },
                    { new Guid("875540ba-362d-4fa9-b2c1-3cd56420e3f9"), new DateTime(2025, 4, 23, 13, 37, 59, 64, DateTimeKind.Utc).AddTicks(4760), "This is another sample product.", "Another Product", 15.99m, new DateTime(2025, 4, 23, 13, 37, 59, 64, DateTimeKind.Utc).AddTicks(4760) }
                });
        }
    }
}
