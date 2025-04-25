using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerceCart.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserAddresses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderVsProduct_Orders_OrderId",
                table: "OrderVsProduct"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_OrderVsProduct_Products_ProductId",
                table: "OrderVsProduct"
            );

            migrationBuilder.DropPrimaryKey(name: "PK_OrderVsProduct", table: "OrderVsProduct");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("1047321a-ff93-429c-83fe-6c812fc7b69c")
            );

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("ddf8d63f-1ce3-4223-bab1-af388cf82015")
            );

            migrationBuilder.RenameTable(
                name: "OrderVsProduct",
                newName: "OrderVsProducts",
                newSchema: "dbo"
            );

            migrationBuilder.RenameIndex(
                name: "IX_OrderVsProduct_ProductId",
                schema: "dbo",
                table: "OrderVsProducts",
                newName: "IX_OrderVsProducts_ProductId"
            );

            migrationBuilder.RenameIndex(
                name: "IX_OrderVsProduct_OrderId",
                schema: "dbo",
                table: "OrderVsProducts",
                newName: "IX_OrderVsProducts_OrderId"
            );

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderVsProducts",
                schema: "dbo",
                table: "OrderVsProducts",
                column: "OrderVsProductId"
            );

            migrationBuilder.CreateTable(
                name: "Addresses",
                schema: "dbo",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserId = table.Column<Guid>(
                        type: "uniqueidentifier",
                        nullable: true
                    ),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Addresses_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id"
                    );
                }
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
                        new Guid("865f7dac-70bd-4d60-869e-53b2be680aa5"),
                        new DateTime(2025, 4, 25, 8, 45, 43, 338, DateTimeKind.Utc).AddTicks(6940),
                        "This is a sample product.",
                        "Sample Product",
                        10.99m,
                        new DateTime(2025, 4, 25, 8, 45, 43, 338, DateTimeKind.Utc).AddTicks(6940),
                    },
                    {
                        new Guid("9234acf6-0800-423f-8bbc-7722354ab26b"),
                        new DateTime(2025, 4, 25, 8, 45, 43, 338, DateTimeKind.Utc).AddTicks(6950),
                        "This is another sample product.",
                        "Another Product",
                        15.99m,
                        new DateTime(2025, 4, 25, 8, 45, 43, 338, DateTimeKind.Utc).AddTicks(6950),
                    },
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ApplicationUserId",
                schema: "dbo",
                table: "Addresses",
                column: "ApplicationUserId"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_OrderVsProducts_Orders_OrderId",
                schema: "dbo",
                table: "OrderVsProducts",
                column: "OrderId",
                principalSchema: "dbo",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_OrderVsProducts_Products_ProductId",
                schema: "dbo",
                table: "OrderVsProducts",
                column: "ProductId",
                principalSchema: "dbo",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderVsProducts_Orders_OrderId",
                schema: "dbo",
                table: "OrderVsProducts"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_OrderVsProducts_Products_ProductId",
                schema: "dbo",
                table: "OrderVsProducts"
            );

            migrationBuilder.DropTable(name: "Addresses", schema: "dbo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderVsProducts",
                schema: "dbo",
                table: "OrderVsProducts"
            );

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("865f7dac-70bd-4d60-869e-53b2be680aa5")
            );

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("9234acf6-0800-423f-8bbc-7722354ab26b")
            );

            migrationBuilder.RenameTable(
                name: "OrderVsProducts",
                schema: "dbo",
                newName: "OrderVsProduct"
            );

            migrationBuilder.RenameIndex(
                name: "IX_OrderVsProducts_ProductId",
                table: "OrderVsProduct",
                newName: "IX_OrderVsProduct_ProductId"
            );

            migrationBuilder.RenameIndex(
                name: "IX_OrderVsProducts_OrderId",
                table: "OrderVsProduct",
                newName: "IX_OrderVsProduct_OrderId"
            );

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderVsProduct",
                table: "OrderVsProduct",
                column: "OrderVsProductId"
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
                        new Guid("1047321a-ff93-429c-83fe-6c812fc7b69c"),
                        new DateTime(2025, 4, 25, 8, 39, 58, 759, DateTimeKind.Utc).AddTicks(9110),
                        "This is another sample product.",
                        "Another Product",
                        15.99m,
                        new DateTime(2025, 4, 25, 8, 39, 58, 759, DateTimeKind.Utc).AddTicks(9110),
                    },
                    {
                        new Guid("ddf8d63f-1ce3-4223-bab1-af388cf82015"),
                        new DateTime(2025, 4, 25, 8, 39, 58, 759, DateTimeKind.Utc).AddTicks(9090),
                        "This is a sample product.",
                        "Sample Product",
                        10.99m,
                        new DateTime(2025, 4, 25, 8, 39, 58, 759, DateTimeKind.Utc).AddTicks(9090),
                    },
                }
            );

            migrationBuilder.AddForeignKey(
                name: "FK_OrderVsProduct_Orders_OrderId",
                table: "OrderVsProduct",
                column: "OrderId",
                principalSchema: "dbo",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_OrderVsProduct_Products_ProductId",
                table: "OrderVsProduct",
                column: "ProductId",
                principalSchema: "dbo",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade
            );
        }
    }
}
