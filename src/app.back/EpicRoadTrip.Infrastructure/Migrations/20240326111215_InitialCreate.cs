using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EpicRoadTrip.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transportations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cost = table.Column<double>(type: "float", nullable: false),
                    Score = table.Column<double>(type: "float", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransportationType = table.Column<int>(type: "int", nullable: false),
                    RouteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transportations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roadtrip",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Budget = table.Column<double>(type: "float", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    NbTransfers = table.Column<int>(type: "int", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Co2Emission = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roadtrip", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roadtrip_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Institution",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreviewUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    WebSite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lat = table.Column<float>(type: "real", nullable: false),
                    Lng = table.Column<float>(type: "real", nullable: false),
                    RoadTripId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institution", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Institution_Roadtrip_RoadTripId",
                        column: x => x.RoadTripId,
                        principalTable: "Roadtrip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Distance = table.Column<double>(type: "float", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    CityOneName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityTwoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RouteGroup = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoadtripId = table.Column<int>(type: "int", nullable: false),
                    GeoJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransportType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Routes_Roadtrip_RoadtripId",
                        column: x => x.RoadtripId,
                        principalTable: "Roadtrip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Institution_RoadTripId",
                table: "Institution",
                column: "RoadTripId");

            migrationBuilder.CreateIndex(
                name: "IX_Roadtrip_UserId",
                table: "Roadtrip",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_RoadtripId",
                table: "Routes",
                column: "RoadtripId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Institution");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "Transportations");

            migrationBuilder.DropTable(
                name: "Roadtrip");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
