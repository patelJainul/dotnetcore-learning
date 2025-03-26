using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Countries",
                schema: "dbo",
                columns: table => new
                {
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                schema: "dbo",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReceiveNewsLetters = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonId);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Countries",
                columns: new[] { "CountryId", "CountryName" },
                values: new object[,]
                {
                    { new Guid("04be259f-882a-4b5b-a794-7d6163b455a0"), "Mexico" },
                    { new Guid("0c080288-b37d-4bfc-a6e7-0b5c27f15b58"), "Chile" },
                    { new Guid("329b11fe-ca2b-448f-9048-a88bf8a08bf4"), "Brazil" },
                    { new Guid("4b6ed032-832b-4ced-9936-bcd23b990ab1"), "Colombia" },
                    { new Guid("7cb1b263-45ff-4fe7-bec7-a036d57fcf4b"), "Venezuela" },
                    { new Guid("94841d04-3c8a-483e-8a7c-964e7c4ab980"), "United States" },
                    { new Guid("9de3383f-8f19-4457-99fc-55f1225f73ad"), "Canada" },
                    { new Guid("c9904974-2bb3-4006-a592-de8325b1a259"), "Argentina" },
                    { new Guid("d2f95305-4080-48b5-9815-041701f0dd8a"), "Peru" },
                    { new Guid("fdb3abe1-2a52-4257-a5e6-b308c6f6d8f2"), "Ecuador" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Persons",
                columns: new[] { "PersonId", "Address", "CountryId", "DateOfBirth", "Email", "FirstName", "Gender", "LastName", "ReceiveNewsLetters" },
                values: new object[,]
                {
                    { new Guid("2e539996-f778-4178-ac9c-6961d4b09e3e"), "3847 Helena Point", new Guid("04be259f-882a-4b5b-a794-7d6163b455a0"), new DateTime(1974, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "pfishbourn1@princeton.edu", "Perla", "Female", "Fishbourn", false },
                    { new Guid("3396831c-f56a-4dc6-acd8-8427b0acd6a2"), "8685 Thierer Park", new Guid("d2f95305-4080-48b5-9815-041701f0dd8a"), new DateTime(1989, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "jrolf9@europa.eu", "Jose", "Male", "Rolf", false },
                    { new Guid("417287cc-d010-489c-bc03-18dfcf78ef61"), "4758 Hintze Park", new Guid("329b11fe-ca2b-448f-9048-a88bf8a08bf4"), new DateTime(1987, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "fescalero3@accuweather.com", "Fredelia", "Female", "Escalero", false },
                    { new Guid("64518ec5-3dc7-4e62-8568-b4ead3acb1ac"), "83143 Meadow Valley Hill", new Guid("c9904974-2bb3-4006-a592-de8325b1a259"), new DateTime(1991, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "benzley4@tuttocitta.it", "Boniface", "Male", "enzley", true },
                    { new Guid("7a6c6d56-f750-4ecf-96f9-a030dd88044e"), "549 Bartelt Trail", new Guid("fdb3abe1-2a52-4257-a5e6-b308c6f6d8f2"), new DateTime(1976, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "ipedrielli7@mysql.com", "Iormina", "Female", "Pedrielli", false },
                    { new Guid("99ed9d3f-108b-4736-b585-bfece71a9052"), "1 Sauthoff Lane", new Guid("4b6ed032-832b-4ced-9936-bcd23b990ab1"), new DateTime(1995, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "gwakenshaw6@ihg.com", "Garwin", "Male", "Wakenshaw", false },
                    { new Guid("a7501f6d-59ba-40fd-888a-497b31feee33"), "3873 Mallard Pass", new Guid("94841d04-3c8a-483e-8a7c-964e7c4ab980"), new DateTime(1985, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "lfuidge2@addtoany.com", "Lory", "Female", "Fuidge", false },
                    { new Guid("b15a98f1-7b80-4239-9d1e-197600a4d962"), "4331 Hallows Pass", new Guid("7cb1b263-45ff-4fe7-bec7-a036d57fcf4b"), new DateTime(1987, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "djahan8@cornell.edu", "Duffie", "Male", "Jahan", false },
                    { new Guid("d00a0458-7794-4d9c-b6b8-7eb4c70f9188"), "2443 Derek Way", new Guid("9de3383f-8f19-4457-99fc-55f1225f73ad"), new DateTime(1984, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mblanc0@jigsy.com", "Malcolm", "Male", "Blanc", false },
                    { new Guid("d860aace-44d7-436e-864a-d04215322860"), "42454 Gerald Crossing", new Guid("0c080288-b37d-4bfc-a6e7-0b5c27f15b58"), new DateTime(1975, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "vpoultney5@delicious.com", "Valle", "Male", "Poultney", false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Countries",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Persons",
                schema: "dbo");
        }
    }
}
