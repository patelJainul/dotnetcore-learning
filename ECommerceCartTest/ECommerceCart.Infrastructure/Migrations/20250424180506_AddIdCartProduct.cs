using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerceCart.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIdCartProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CartVsProducts",
                schema: "dbo",
                table: "CartVsProducts");

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

            migrationBuilder.AddColumn<Guid>(
                name: "CartVsProductsId",
                schema: "dbo",
                table: "CartVsProducts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartVsProducts",
                schema: "dbo",
                table: "CartVsProducts",
                column: "CartVsProductsId");

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Products",
                columns: new[] { "ProductId", "CreatedAt", "Description", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("835120a6-728d-44ea-9980-c2ecf1af23f3"), new DateTime(2025, 4, 24, 18, 5, 6, 99, DateTimeKind.Utc).AddTicks(5340), "This is a sample product.", "Sample Product", 10.99m, new DateTime(2025, 4, 24, 18, 5, 6, 99, DateTimeKind.Utc).AddTicks(5340) },
                    { new Guid("dac60585-43cb-45dd-84aa-8d79bfe2385f"), new DateTime(2025, 4, 24, 18, 5, 6, 99, DateTimeKind.Utc).AddTicks(5380), "This is another sample product.", "Another Product", 15.99m, new DateTime(2025, 4, 24, 18, 5, 6, 99, DateTimeKind.Utc).AddTicks(5380) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartVsProducts_CartId",
                schema: "dbo",
                table: "CartVsProducts",
                column: "CartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CartVsProducts",
                schema: "dbo",
                table: "CartVsProducts");

            migrationBuilder.DropIndex(
                name: "IX_CartVsProducts_CartId",
                schema: "dbo",
                table: "CartVsProducts");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("835120a6-728d-44ea-9980-c2ecf1af23f3"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("dac60585-43cb-45dd-84aa-8d79bfe2385f"));

            migrationBuilder.DropColumn(
                name: "CartVsProductsId",
                schema: "dbo",
                table: "CartVsProducts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartVsProducts",
                schema: "dbo",
                table: "CartVsProducts",
                columns: new[] { "CartId", "ProductId" });

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
    }
}
