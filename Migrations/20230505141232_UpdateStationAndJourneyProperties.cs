using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace solita_assignment.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStationAndJourneyProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StationName",
                table: "Stations",
                newName: "Operator");

            migrationBuilder.RenameColumn(
                name: "StationAddress",
                table: "Stations",
                newName: "NameSwedish");

            migrationBuilder.AddColumn<string>(
                name: "AddressFinnish",
                table: "Stations",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressSwedish",
                table: "Stations",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Stations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CityFinnish",
                table: "Stations",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CitySwedish",
                table: "Stations",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdInt",
                table: "Stations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "LocationX",
                table: "Stations",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LocationY",
                table: "Stations",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "NameEnglish",
                table: "Stations",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameFinnish",
                table: "Stations",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "CoveredDistance",
                table: "Journeys",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "REAL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressFinnish",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "AddressSwedish",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "CityFinnish",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "CitySwedish",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "IdInt",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "LocationX",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "LocationY",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "NameEnglish",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "NameFinnish",
                table: "Stations");

            migrationBuilder.RenameColumn(
                name: "Operator",
                table: "Stations",
                newName: "StationName");

            migrationBuilder.RenameColumn(
                name: "NameSwedish",
                table: "Stations",
                newName: "StationAddress");

            migrationBuilder.AlterColumn<double>(
                name: "CoveredDistance",
                table: "Journeys",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldNullable: true);
        }
    }
}
