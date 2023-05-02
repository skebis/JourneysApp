using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace solita_assignment.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Journeys",
                columns: table => new
                {
                    JourneyId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Departure = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Return = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DepartureStationId = table.Column<int>(type: "INTEGER", nullable: false),
                    DepartureStationName = table.Column<string>(type: "TEXT", nullable: true),
                    ReturnStationId = table.Column<int>(type: "INTEGER", nullable: false),
                    ReturnStationName = table.Column<string>(type: "TEXT", nullable: true),
                    CoveredDistance = table.Column<int>(type: "INTEGER", nullable: false),
                    Duration = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journeys", x => x.JourneyId);
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    StationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    StationName = table.Column<string>(type: "TEXT", nullable: true),
                    StationAddress = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.StationId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Journeys");

            migrationBuilder.DropTable(
                name: "Stations");
        }
    }
}
