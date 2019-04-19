using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartPlate.API.Migrations
{
    public partial class AddPasswordSaltToUsersTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Users",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "TrafficUsers",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Officers",
                nullable: false,
                defaultValue: new byte[] {  });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "TrafficUsers");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Officers");
        }
    }
}
