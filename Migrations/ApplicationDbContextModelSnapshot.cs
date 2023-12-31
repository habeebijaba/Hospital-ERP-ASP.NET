﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FunInVscode.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Availability")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Charge")
                        .HasColumnType("int");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Doctors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Availability = "daily",
                            Charge = 200,
                            Department = "nephrology",
                            ImagePath = "",
                            Name = "habeebav841"
                        },
                        new
                        {
                            Id = 2,
                            Availability = "daily",
                            Charge = 200,
                            Department = "orthology",
                            ImagePath = "",
                            Name = "safeer"
                        },
                        new
                        {
                            Id = 3,
                            Availability = "daily",
                            Charge = 200,
                            Department = "orthology",
                            ImagePath = "",
                            Name = "saleel"
                        });
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Place")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Age = 33,
                            DateOfBirth = new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "habeebav841@gmail.com",
                            Image = "user1.jpg",
                            Name = "Habeeb Ijaba",
                            Password = "123",
                            Place = "SomePlace"
                        },
                        new
                        {
                            Id = 2,
                            Age = 32,
                            DateOfBirth = new DateTime(1991, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "safeer@gmail.com",
                            Image = "user2.jpg",
                            Name = "Safeer sfr",
                            Password = "123",
                            Place = "AnotherPlace"
                        },
                        new
                        {
                            Id = 3,
                            Age = 31,
                            DateOfBirth = new DateTime(1992, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "saleel@gmail.com",
                            Image = "user3.jpg",
                            Name = "Saleel Hisan",
                            Password = "123",
                            Place = "YetAnotherPlace"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
