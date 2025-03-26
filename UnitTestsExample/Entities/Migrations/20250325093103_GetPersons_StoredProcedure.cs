using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class GetPersons_StoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE PROCEDURE [dbo].[GetPersons]
                AS
                BEGIN
                    SELECT PersonId, FirstName, LastName, Email, DateOfBirth, Gender, CountryId, Address, ReceiveNewsLetters
                    FROM [dbo].[Persons]
                END"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE [dbo].[GetPersons]");
        }
    }
}
