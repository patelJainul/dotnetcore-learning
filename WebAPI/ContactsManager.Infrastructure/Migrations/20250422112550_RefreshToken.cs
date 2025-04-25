using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ContactsManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Cities",
                keyColumn: "CityId",
                keyValue: new Guid("499f6431-9bd4-4c1b-8932-f262b0c169ec")
            );

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Cities",
                keyColumn: "CityId",
                keyValue: new Guid("ba26c67f-2f34-4e2b-98a5-161e36ff5a53")
            );

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true
            );

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(name: "RefreshToken", table: "AspNetUsers");

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Cities",
                columns: new[] { "CityId", "Name" },
                values: new object[,]
                {
                    { new Guid("499f6431-9bd4-4c1b-8932-f262b0c169ec"), "Los Angeles" },
                    { new Guid("ba26c67f-2f34-4e2b-98a5-161e36ff5a53"), "New York" },
                }
            );
        }
    }
}
