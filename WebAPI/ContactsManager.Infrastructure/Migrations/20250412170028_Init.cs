using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ContactsManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(name: "dbo");

            migrationBuilder.CreateTable(
                name: "Cities",
                schema: "dbo",
                columns: table => new
                {
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityId);
                }
            );

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Cities",
                columns: new[] { "CityId", "Name" },
                values: new object[,]
                {
                    { new Guid("553b2e05-d568-43d3-bb5e-a20511dbb694"), "New York" },
                    { new Guid("e1f6827d-4ad4-4e09-8ec3-50fe2e3396f6"), "Los Angeles" },
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Cities", schema: "dbo");
        }
    }
}
