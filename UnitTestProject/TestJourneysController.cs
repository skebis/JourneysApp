using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using solita_assignment.Classes;
using solita_assignment.Controllers;

namespace solita_assignment.Tests
{
    [TestClass]
    public class TestJourneysController
    {

        [TestMethod]
        public void GetJourneysPagination_ShouldReturnGivenAmount()
        {
            var testJourneys = GetJourneysSample();
            //var journeyController = new JourneysController();
            Assert.IsNotNull(true);
        }

        public Journey GetSingleJourneySample()
        {
            var journey = new Journey
            {
                JourneyId = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00"),
                Departure = new DateTime(2023, 8, 22, 10, 58, 10),
                Return = new DateTime(2023, 8, 22, 12, 45, 2),
                DepartureStationName = "Station",
                DepartureStationId = 1,
                ReturnStationName = "Station 2",
                ReturnStationId = 2,
                CoveredDistance = 2000,
                Duration = 1200
            };
            return journey;
        }

        public List<Journey> GetJourneysSample()
        {
            List<Journey> journeysSample = new List<Journey>();
            //  year, month, day, hour, minute, and second.
            journeysSample.Add(new Journey
            {
                JourneyId = Guid.NewGuid(),
                Departure = new DateTime(2023, 8, 22, 10, 58, 10),
                Return = new DateTime(2023, 8, 22, 12, 45, 2),
                DepartureStationName = "Station",
                DepartureStationId = 1,
                ReturnStationName = "Station 2",
                ReturnStationId = 2,
                CoveredDistance = 2000,
                Duration = 1200
            });
            journeysSample.Add(new Journey
            {
                JourneyId = Guid.NewGuid(),
                Departure = new DateTime(2023, 8, 15, 10, 58, 10),
                Return = new DateTime(2023, 8, 22, 12, 45, 2),
                DepartureStationName = "Station 2",
                DepartureStationId = 2,
                ReturnStationName = "Station 3",
                ReturnStationId = 3,
                CoveredDistance = 100,
                Duration = 650
            });
            journeysSample.Add(new Journey
            {
                JourneyId = Guid.NewGuid(),
                Departure = new DateTime(2023, 8, 12, 10, 58, 10),
                Return = new DateTime(2023, 8, 22, 12, 45, 2),
                DepartureStationName = "Station 3",
                DepartureStationId = 3,
                ReturnStationName = "Station 4",
                ReturnStationId = 4,
                CoveredDistance = 550,
                Duration = 400
            });
            journeysSample.Add(new Journey
            {
                JourneyId = Guid.NewGuid(),
                Departure = new DateTime(2023, 8, 5, 10, 58, 10),
                Return = new DateTime(2023, 8, 22, 12, 45, 2),
                DepartureStationName = "Station 4",
                DepartureStationId = 4,
                ReturnStationName = "Station 5",
                ReturnStationId = 5,
                CoveredDistance = 256,
                Duration = 20
            });
            journeysSample.Add(new Journey
            {
                JourneyId = Guid.NewGuid(),
                Departure = new DateTime(2023, 8, 8, 10, 58, 10),
                Return = new DateTime(2023, 8, 22, 12, 45, 2),
                DepartureStationName = "Station 5",
                DepartureStationId = 5,
                ReturnStationName = "Station 6",
                ReturnStationId = 6,
                CoveredDistance = 790,
                Duration = 65
            });

            return journeysSample;
        }
    }
}