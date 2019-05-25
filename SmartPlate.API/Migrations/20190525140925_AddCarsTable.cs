using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartPlate.API.Migrations
{
    public partial class AddCarsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PlateNumber = table.Column<string>(nullable: false),
                    ShaseehNumber = table.Column<string>(nullable: false),
                    MotorNumber = table.Column<string>(nullable: false),
                    Color = table.Column<string>(nullable: false),
                    ModelYear = table.Column<int>(nullable: false),
                    ModelMarka = table.Column<string>(nullable: false),
                    CarModel = table.Column<string>(nullable: false),
                    VechileType = table.Column<string>(nullable: false),
                    Fuel = table.Column<string>(nullable: false),
                    Capacity = table.Column<int>(nullable: false),
                    Passengers = table.Column<int>(nullable: false),
                    LoadWeight = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    TrafficId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Traffics_TrafficId",
                        column: x => x.TrafficId,
                        principalTable: "Traffics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cars_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_TrafficId",
                table: "Cars",
                column: "TrafficId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_UserId",
                table: "Cars",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}
