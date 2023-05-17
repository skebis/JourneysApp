using CsvHelper.Configuration.Attributes;

namespace solita_assignment.Classes
{
    // Represents a single station object.
    public class Station
    {
        public Guid StationId { get; set; }

        public int IdInt { get; set; }

        public string? NameFinnish { get; set; }

        public string? NameSwedish { get; set; }

        public string? NameEnglish { get; set; }

        public string? AddressFinnish { get; set; }

        public string? AddressSwedish { get; set; }

        public string? CityFinnish { get; set; }

        public string? CitySwedish { get; set; }

        public string? Operator { get; set; }

        public int Capacity { get; set; }

        public decimal LocationX { get; set; }

        public decimal LocationY { get; set; }
    }
    public class StationDto
    {

        [Index(1)]
        public int IdInt { get; set; }

        [Index(2)]
        public string? NameFinnish { get; set; }

        [Index(3)]
        public string? NameSwedish { get; set; }

        [Index(4)]
        public string? NameEnglish { get; set; }

        [Index(5)]
        public string? AddressFinnish { get; set; }

        [Index(6)]
        public string? AddressSwedish { get; set; }

        [Index(7)]
        public string? CityFinnish { get; set; }

        [Index(8)]
        public string? CitySwedish { get; set; }

        [Index(9)]
        public string? Operator { get; set; }

        [Index(10)]
        public int Capacity { get; set; }

        [Index(11)]
        public decimal LocationX { get; set; }

        [Index(12)]
        public decimal LocationY { get; set; }
    }
}
