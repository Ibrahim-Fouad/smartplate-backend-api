using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartPlate.API.Migrations
{
    public partial class CreateUsersTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Officers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    BloodType = table.Column<string>(maxLength: 5, nullable: false),
                    EducationalQualification = table.Column<string>(nullable: false),
                    PasswordHashed = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Officers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrafficUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    BloodType = table.Column<string>(maxLength: 5, nullable: false),
                    EducationalQualification = table.Column<string>(nullable: false),
                    PasswordHashed = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrafficUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    BloodType = table.Column<string>(maxLength: 5, nullable: false),
                    EducationalQualification = table.Column<string>(nullable: false),
                    PasswordHashed = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Officers");

            migrationBuilder.DropTable(
                name: "TrafficUsers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
