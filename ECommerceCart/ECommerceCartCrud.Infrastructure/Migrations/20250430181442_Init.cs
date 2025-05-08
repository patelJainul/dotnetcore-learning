using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerceCartCrud.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Carts",
                schema: "dbo",
                columns: table => new
                {
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.CartId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "dbo",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "CartVsProducts",
                schema: "dbo",
                columns: table => new
                {
                    CartVsProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartVsProducts", x => x.CartVsProductId);
                    table.ForeignKey(
                        name: "FK_CartVsProducts_Carts_CartId",
                        column: x => x.CartId,
                        principalSchema: "dbo",
                        principalTable: "Carts",
                        principalColumn: "CartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartVsProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "dbo",
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Products",
                columns: new[] { "ProductId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("72a86086-6243-48fd-991c-41f5ec37efd3"), "Description for Product 1", "Product 1", 10.99 },
                    { new Guid("ee72e4e3-1096-4917-bde5-880d25da637d"), "Description for Product 2", "Product 2", 12.67 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartVsProducts_CartId",
                schema: "dbo",
                table: "CartVsProducts",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartVsProducts_ProductId",
                schema: "dbo",
                table: "CartVsProducts",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartVsProducts",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Carts",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "dbo");
        }
    }
}
