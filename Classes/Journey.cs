using CsvHelper.Configuration.Attributes;

namespace solita_assignment.Classes
{
    public class Journey
    {
        public Guid JourneyId { get; set; }

        [Index(0)]
        public DateTime Departure { get; set; }

        [Index(1)]
        public DateTime Return { get; set; }

        [Index(2)]
        public int DepartureStationId { get; set; }

        [Index(3)]
        public string? DepartureStationName { get; set; }

        [Index(4)]
        public int ReturnStationId { get; set; }

        [Index(5)]
        public string? ReturnStationName { get; set; }

        [Index(6)]
        public double CoveredDistance { get; set; }

        [Index(7)]
        public int Duration { get; set; }
    }

    public class JourneyDto
    {
        [Index(0)]
        public DateTime Departure { get; set; }

        [Index(1)]
        public DateTime Return { get; set; }

        [Index(2)]
        public int DepartureStationId { get; set; }

        [Index(3)]
        public string? DepartureStationName { get; set; }

        [Index(4)]
        public int ReturnStationId { get; set; }

        [Index(5)]
        public string? ReturnStationName { get; set; }

        [Index(6)]
        public double CoveredDistance { get; set; }

        [Index(7)]
        public int Duration { get; set; }
    }
}
