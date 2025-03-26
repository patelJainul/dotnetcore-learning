﻿// <auto-generated />
using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Entities.Migrations
{
    [DbContext(typeof(ContactDbContext))]
    [Migration("20250325093030_InsertPerson_StoredProcedure")]
    partial class InsertPerson_StoredProcedure
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.Country", b =>
                {
                    b.Property<Guid>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CountryId");

                    b.ToTable("Countries", "dbo");

                    b.HasData(
                        new
                        {
                            CountryId = new Guid("9de3383f-8f19-4457-99fc-55f1225f73ad"),
                            CountryName = "Canada"
                        },
                        new
                        {
                            CountryId = new Guid("04be259f-882a-4b5b-a794-7d6163b455a0"),
                            CountryName = "Mexico"
                        },
                        new
                        {
                            CountryId = new Guid("94841d04-3c8a-483e-8a7c-964e7c4ab980"),
                            CountryName = "United States"
                        },
                        new
                        {
                            CountryId = new Guid("329b11fe-ca2b-448f-9048-a88bf8a08bf4"),
                            CountryName = "Brazil"
                        },
                        new
                        {
                            CountryId = new Guid("c9904974-2bb3-4006-a592-de8325b1a259"),
                            CountryName = "Argentina"
                        },
                        new
                        {
                            CountryId = new Guid("0c080288-b37d-4bfc-a6e7-0b5c27f15b58"),
                            CountryName = "Chile"
                        },
                        new
                        {
                            CountryId = new Guid("4b6ed032-832b-4ced-9936-bcd23b990ab1"),
                            CountryName = "Colombia"
                        },
                        new
                        {
                            CountryId = new Guid("fdb3abe1-2a52-4257-a5e6-b308c6f6d8f2"),
                            CountryName = "Ecuador"
                        },
                        new
                        {
                            CountryId = new Guid("7cb1b263-45ff-4fe7-bec7-a036d57fcf4b"),
                            CountryName = "Venezuela"
                        },
                        new
                        {
                            CountryId = new Guid("d2f95305-4080-48b5-9815-041701f0dd8a"),
                            CountryName = "Peru"
                        });
                });

            modelBuilder.Entity("Entities.Person", b =>
                {
                    b.Property<Guid>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid?>("CountryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Gender")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<bool>("ReceiveNewsLetters")
                        .HasColumnType("bit");

                    b.HasKey("PersonId");

                    b.ToTable("Persons", "dbo");

                    b.HasData(
                        new
                        {
                            PersonId = new Guid("d00a0458-7794-4d9c-b6b8-7eb4c70f9188"),
                            Address = "2443 Derek Way",
                            CountryId = new Guid("9de3383f-8f19-4457-99fc-55f1225f73ad"),
                            DateOfBirth = new DateTime(1984, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "mblanc0@jigsy.com",
                            FirstName = "Malcolm",
                            Gender = "Male",
                            LastName = "Blanc",
                            ReceiveNewsLetters = false
                        },
                        new
                        {
                            PersonId = new Guid("2e539996-f778-4178-ac9c-6961d4b09e3e"),
                            Address = "3847 Helena Point",
                            CountryId = new Guid("04be259f-882a-4b5b-a794-7d6163b455a0"),
                            DateOfBirth = new DateTime(1974, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "pfishbourn1@princeton.edu",
                            FirstName = "Perla",
                            Gender = "Female",
                            LastName = "Fishbourn",
                            ReceiveNewsLetters = false
                        },
                        new
                        {
                            PersonId = new Guid("a7501f6d-59ba-40fd-888a-497b31feee33"),
                            Address = "3873 Mallard Pass",
                            CountryId = new Guid("94841d04-3c8a-483e-8a7c-964e7c4ab980"),
                            DateOfBirth = new DateTime(1985, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "lfuidge2@addtoany.com",
                            FirstName = "Lory",
                            Gender = "Female",
                            LastName = "Fuidge",
                            ReceiveNewsLetters = false
                        },
                        new
                        {
                            PersonId = new Guid("417287cc-d010-489c-bc03-18dfcf78ef61"),
                            Address = "4758 Hintze Park",
                            CountryId = new Guid("329b11fe-ca2b-448f-9048-a88bf8a08bf4"),
                            DateOfBirth = new DateTime(1987, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "fescalero3@accuweather.com",
                            FirstName = "Fredelia",
                            Gender = "Female",
                            LastName = "Escalero",
                            ReceiveNewsLetters = false
                        },
                        new
                        {
                            PersonId = new Guid("64518ec5-3dc7-4e62-8568-b4ead3acb1ac"),
                            Address = "83143 Meadow Valley Hill",
                            CountryId = new Guid("c9904974-2bb3-4006-a592-de8325b1a259"),
                            DateOfBirth = new DateTime(1991, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "benzley4@tuttocitta.it",
                            FirstName = "Boniface",
                            Gender = "Male",
                            LastName = "enzley",
                            ReceiveNewsLetters = true
                        },
                        new
                        {
                            PersonId = new Guid("d860aace-44d7-436e-864a-d04215322860"),
                            Address = "42454 Gerald Crossing",
                            CountryId = new Guid("0c080288-b37d-4bfc-a6e7-0b5c27f15b58"),
                            DateOfBirth = new DateTime(1975, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "vpoultney5@delicious.com",
                            FirstName = "Valle",
                            Gender = "Male",
                            LastName = "Poultney",
                            ReceiveNewsLetters = false
                        },
                        new
                        {
                            PersonId = new Guid("99ed9d3f-108b-4736-b585-bfece71a9052"),
                            Address = "1 Sauthoff Lane",
                            CountryId = new Guid("4b6ed032-832b-4ced-9936-bcd23b990ab1"),
                            DateOfBirth = new DateTime(1995, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "gwakenshaw6@ihg.com",
                            FirstName = "Garwin",
                            Gender = "Male",
                            LastName = "Wakenshaw",
                            ReceiveNewsLetters = false
                        },
                        new
                        {
                            PersonId = new Guid("7a6c6d56-f750-4ecf-96f9-a030dd88044e"),
                            Address = "549 Bartelt Trail",
                            CountryId = new Guid("fdb3abe1-2a52-4257-a5e6-b308c6f6d8f2"),
                            DateOfBirth = new DateTime(1976, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "ipedrielli7@mysql.com",
                            FirstName = "Iormina",
                            Gender = "Female",
                            LastName = "Pedrielli",
                            ReceiveNewsLetters = false
                        },
                        new
                        {
                            PersonId = new Guid("b15a98f1-7b80-4239-9d1e-197600a4d962"),
                            Address = "4331 Hallows Pass",
                            CountryId = new Guid("7cb1b263-45ff-4fe7-bec7-a036d57fcf4b"),
                            DateOfBirth = new DateTime(1987, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "djahan8@cornell.edu",
                            FirstName = "Duffie",
                            Gender = "Male",
                            LastName = "Jahan",
                            ReceiveNewsLetters = false
                        },
                        new
                        {
                            PersonId = new Guid("3396831c-f56a-4dc6-acd8-8427b0acd6a2"),
                            Address = "8685 Thierer Park",
                            CountryId = new Guid("d2f95305-4080-48b5-9815-041701f0dd8a"),
                            DateOfBirth = new DateTime(1989, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "jrolf9@europa.eu",
                            FirstName = "Jose",
                            Gender = "Male",
                            LastName = "Rolf",
                            ReceiveNewsLetters = false
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
