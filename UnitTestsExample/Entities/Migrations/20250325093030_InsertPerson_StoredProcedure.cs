using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class InsertPerson_StoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE PROCEDURE [dbo].[InsertPerson]
                @PersonId UNIQUEIDENTIFIER OUTPUT,
                @FirstName NVARCHAR(40),
                @LastName NVARCHAR(40),
                @Email NVARCHAR(50),
                @DateOfBirth DATETIME2,
                @Gender NVARCHAR(10),
                @CountryId UNIQUEIDENTIFIER,
                @Address NVARCHAR(200),
                @ReceiveNewsLetters BIT
                AS
                BEGIN
                    INSERT INTO [dbo].[Persons] (PersonId, FirstName, LastName, Email, DateOfBirth, Gender, CountryId, Address, ReceiveNewsLetters)
                    VALUES (@PersonId, @FirstName, @LastName, @Email, @DateOfBirth, @Gender, @CountryId, @Address, @ReceiveNewsLetters);
                END"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE [dbo].[InsertPerson]");
        }
    }
}
