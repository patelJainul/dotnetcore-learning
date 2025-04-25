using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerceCart.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CartProductQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("7deb1f8f-4365-44d3-81be-220fa11d7c3d"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("83627b02-6f66-4312-9acd-78afa765e5c7"));

            migrationBuilder.DropColumn(
                name: "Quantity",
                schema: "dbo",
                table: "Carts");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                schema: "dbo",
                table: "CartVsProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Quantity",
                schema: "dbo",
                table: "CartVsProducts");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                schema: "dbo",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Products",
                columns: new[] { "ProductId", "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("7deb1f8f-4365-44d3-81be-220fa11d7c3d"), new DateTime(2025, 4, 23, 9, 53, 25, 490, DateTimeKind.Utc).AddTicks(5990), "This is a sample product.", "Sample Product", new DateTime(2025, 4, 23, 9, 53, 25, 490, DateTimeKind.Utc).AddTicks(5990) },
                    { new Guid("83627b02-6f66-4312-9acd-78afa765e5c7"), new DateTime(2025, 4, 23, 9, 53, 25, 490, DateTimeKind.Utc).AddTicks(6010), "This is another sample product.", "Another Product", new DateTime(2025, 4, 23, 9, 53, 25, 490, DateTimeKind.Utc).AddTicks(6010) }
                });
        }
    }
}
