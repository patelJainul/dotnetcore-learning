using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerceCart.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovedCartVsProductFromProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "dbo",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "OrderVsProduct",
                columns: table => new
                {
                    OrderVsProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderVsProduct", x => x.OrderVsProductId);
                    table.ForeignKey(
                        name: "FK_OrderVsProduct_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "dbo",
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderVsProduct_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "dbo",
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Products",
                columns: new[] { "ProductId", "CreatedAt", "Description", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("1047321a-ff93-429c-83fe-6c812fc7b69c"), new DateTime(2025, 4, 25, 8, 39, 58, 759, DateTimeKind.Utc).AddTicks(9110), "This is another sample product.", "Another Product", 15.99m, new DateTime(2025, 4, 25, 8, 39, 58, 759, DateTimeKind.Utc).AddTicks(9110) },
                    { new Guid("ddf8d63f-1ce3-4223-bab1-af388cf82015"), new DateTime(2025, 4, 25, 8, 39, 58, 759, DateTimeKind.Utc).AddTicks(9090), "This is a sample product.", "Sample Product", 10.99m, new DateTime(2025, 4, 25, 8, 39, 58, 759, DateTimeKind.Utc).AddTicks(9090) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderVsProduct_OrderId",
                table: "OrderVsProduct",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderVsProduct_ProductId",
                table: "OrderVsProduct",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderVsProduct");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "dbo");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("1047321a-ff93-429c-83fe-6c812fc7b69c"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("ddf8d63f-1ce3-4223-bab1-af388cf82015"));

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Products",
                columns: new[] { "ProductId", "CreatedAt", "Description", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("835120a6-728d-44ea-9980-c2ecf1af23f3"), new DateTime(2025, 4, 24, 18, 5, 6, 99, DateTimeKind.Utc).AddTicks(5340), "This is a sample product.", "Sample Product", 10.99m, new DateTime(2025, 4, 24, 18, 5, 6, 99, DateTimeKind.Utc).AddTicks(5340) },
                    { new Guid("dac60585-43cb-45dd-84aa-8d79bfe2385f"), new DateTime(2025, 4, 24, 18, 5, 6, 99, DateTimeKind.Utc).AddTicks(5380), "This is another sample product.", "Another Product", 15.99m, new DateTime(2025, 4, 24, 18, 5, 6, 99, DateTimeKind.Utc).AddTicks(5380) }
                });
        }
    }
}
