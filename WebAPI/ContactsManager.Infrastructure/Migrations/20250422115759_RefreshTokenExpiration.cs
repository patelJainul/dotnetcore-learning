using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ContactsManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefreshTokenExpiration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Cities",
                keyColumn: "CityId",
                keyValue: new Guid("528c3351-a8fa-4492-a359-de3b67c951cc")
            );

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Cities",
                keyColumn: "CityId",
                keyValue: new Guid("f3ec4de3-3e40-4208-bceb-5871028fa1bc")
            );

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiration",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true
            );

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Cities",
                columns: new[] { "CityId", "Name" },
                values: new object[,]
                {
                    { new Guid("15ba7e62-af81-4f7a-b621-8ce7cb6dcdbe"), "New York" },
                    { new Guid("d7bfd11d-78c1-40de-a1fe-a22ae58dcb4d"), "Los Angeles" },
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Cities",
                keyColumn: "CityId",
                keyValue: new Guid("15ba7e62-af81-4f7a-b621-8ce7cb6dcdbe")
            );

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Cities",
                keyColumn: "CityId",
                keyValue: new Guid("d7bfd11d-78c1-40de-a1fe-a22ae58dcb4d")
            );

            migrationBuilder.DropColumn(name: "RefreshTokenExpiration", table: "AspNetUsers");

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Cities",
                columns: new[] { "CityId", "Name" },
                values: new object[,]
                {
                    { new Guid("528c3351-a8fa-4492-a359-de3b67c951cc"), "Los Angeles" },
                    { new Guid("f3ec4de3-3e40-4208-bceb-5871028fa1bc"), "New York" },
                }
            );
        }
    }
}
