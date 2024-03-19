using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EpicRoadTrip.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RouteMetadataIncrement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transportations_Routes_RouteId",
                table: "Transportations");

            migrationBuilder.DropIndex(
                name: "IX_Transportations_RouteId",
                table: "Transportations");

            migrationBuilder.AddColumn<Guid>(
                name: "RouteGroup",
                table: "Routes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransportType",
                table: "Routes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RouteGroup",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "TransportType",
                table: "Routes");

            migrationBuilder.CreateIndex(
                name: "IX_Transportations_RouteId",
                table: "Transportations",
                column: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transportations_Routes_RouteId",
                table: "Transportations",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
