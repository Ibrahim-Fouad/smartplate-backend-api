﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartPlate.API.Db;

namespace SmartPlate.API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20190606155326_AddStolenCarsTable")]
    partial class AddStolenCarsTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SmartPlate.API.Models.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Capacity");

                    b.Property<string>("CarModel")
                        .IsRequired();

                    b.Property<string>("Color")
                        .IsRequired();

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Fuel")
                        .IsRequired();

                    b.Property<int>("LoadWeight");

                    b.Property<string>("ModelMarka")
                        .IsRequired();

                    b.Property<int>("ModelYear");

                    b.Property<string>("MotorNumber")
                        .IsRequired();

                    b.Property<int>("Passengers");

                    b.Property<string>("PlateNumber")
                        .IsRequired();

                    b.Property<string>("ShaseehNumber")
                        .IsRequired();

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("TrafficId");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.Property<string>("VechileType")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("TrafficId");

                    b.HasIndex("UserId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("SmartPlate.API.Models.StolenCar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CarId");

                    b.Property<byte>("CarOrPlateIsStoled");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateReturened");

                    b.Property<DateTime>("DateStoled");

                    b.Property<bool>("HasReturenedToOwner");

                    b.Property<string>("LastLocation")
                        .IsRequired();

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.ToTable("StolenCars");
                });

            modelBuilder.Entity("SmartPlate.API.Models.Traffic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Email");

                    b.Property<string>("Governorate")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("PhoneNumber");

                    b.HasKey("Id");

                    b.ToTable("Traffics");
                });

            modelBuilder.Entity("SmartPlate.API.Models.Users.Officer", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("BloodType")
                        .IsRequired()
                        .HasMaxLength(5);

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("EducationalQualification")
                        .IsRequired();

                    b.Property<string>("Email");

                    b.Property<string>("ImagePath");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<byte[]>("PasswordHashed")
                        .IsRequired();

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired();

                    b.Property<string>("PhoneNumber");

                    b.HasKey("Id");

                    b.ToTable("Officers");
                });

            modelBuilder.Entity("SmartPlate.API.Models.Users.TrafficUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("BloodType")
                        .IsRequired()
                        .HasMaxLength(5);

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("EducationalQualification")
                        .IsRequired();

                    b.Property<string>("Email");

                    b.Property<string>("ImagePath");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<byte[]>("PasswordHashed")
                        .IsRequired();

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired();

                    b.Property<string>("PhoneNumber");

                    b.HasKey("Id");

                    b.ToTable("TrafficUsers");
                });

            modelBuilder.Entity("SmartPlate.API.Models.Users.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("BloodType")
                        .IsRequired()
                        .HasMaxLength(5);

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("EducationalQualification")
                        .IsRequired();

                    b.Property<string>("Email");

                    b.Property<string>("ImagePath");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<byte[]>("PasswordHashed")
                        .IsRequired();

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired();

                    b.Property<string>("PhoneNumber");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SmartPlate.API.Models.Car", b =>
                {
                    b.HasOne("SmartPlate.API.Models.Traffic", "Traffic")
                        .WithMany("Cars")
                        .HasForeignKey("TrafficId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SmartPlate.API.Models.Users.User", "User")
                        .WithMany("Cars")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SmartPlate.API.Models.StolenCar", b =>
                {
                    b.HasOne("SmartPlate.API.Models.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
