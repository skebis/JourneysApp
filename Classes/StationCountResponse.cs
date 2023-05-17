namespace solita_assignment.Classes
{
    // Represents a single station get response.
    public class StationCountResponse
    {
        public int DepartureStationCount { get; set; }

        public int ReturnStationCount { get; set; }

        public Station Station { get; set; }
    }
}
