using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using solita_assignment.Classes;
using solita_assignment.Controllers;
using solita_assignment.Models;
using Xunit;

namespace solita_assignment.Tests
{
    // Collection is used to disable parallelization, which caused weird failures (possibly because of same DBContext).
    [Collection("Journeys and Stations test collection")]
    public class TestStationsController
    {

        [Fact]
        public async Task GetStations_ShouldReturnGivenAmount()
        {
            // Arrange
            var opt = new DbContextOptionsBuilder<JourneyContext>()
                .UseInMemoryDatabase(databaseName: "JourneysDB")
                .Options;

            using (var context = new JourneyContext(opt))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.AddRange(GetStationsSample());
                context.SaveChanges();
            }

            // Act
            using (var context = new JourneyContext(opt))
            {
                var controller = new StationsController(context);
                var res = (await controller.GetStations()).Value;

                // Assert
                Assert.NotNull(res);
                Assert.Equal(5, res.Count());
            }
        }

        [Fact]
        public async Task GetStation_ShouldReturnRequestedStationAndStationCounts()
        {
            // Arrange
            var opt = new DbContextOptionsBuilder<JourneyContext>()
                .UseInMemoryDatabase(databaseName: "JourneysDB")
                .Options;

            using (var context = new JourneyContext(opt))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Add(GetSingleStationSample());
                context.AddRange(GetJourneysSample());
                context.SaveChanges();
            }

            // Act
            using (var context = new JourneyContext(opt))
            {
                var controller = new StationsController(context);
                var res = (await controller.GetStation(new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Result as OkObjectResult;

                // Assert
                Assert.NotNull(res);
                Assert.IsType<OkObjectResult>(res);

                var resValue = res.Value as StationCountResponse;
                Assert.NotNull(resValue);
                Assert.Equal(1, resValue.DepartureStationCount);
                Assert.Equal(3, resValue.ReturnStationCount);
                Assert.Equal(resValue.Station.StationId, new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00"));
            }
        }

        [Fact]
        public async Task GetStation_StationDoesntExist_ShouldReturnNotFound()
        {
            // Arrange
            var opt = new DbContextOptionsBuilder<JourneyContext>()
                .UseInMemoryDatabase(databaseName: "JourneysDB")
                .Options;

            using (var context = new JourneyContext(opt))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Add(GetSingleStationSample());
                context.SaveChanges();
            }

            // Act
            using (var context = new JourneyContext(opt))
            {
                var controller = new StationsController(context);
                var res = (await controller.GetStation(new Guid("22223344-5566-7788-99AA-BBCCDDEEFF00"))).Result;

                // Assert
                Assert.NotNull(res);
                Assert.IsType<NotFoundResult>(res);
            }
        }

        [Fact]
        public async Task PostStation_ShouldReturnStationCreated()
        {
            // Arrange
            var opt = new DbContextOptionsBuilder<JourneyContext>()
                .UseInMemoryDatabase(databaseName: "JourneysDB")
                .Options;

            // Act
            using (var context = new JourneyContext(opt))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                var controller = new StationsController(context);
                var res = (await controller.PostStation(GetSingleStationDtoSample())).Result;

                // Assert
                Assert.NotNull(res);
                Assert.IsType<CreatedAtActionResult>(res);
            }
        }

        [Fact]
        public async Task PutStation_ValidStationModel_ShouldReturnNoContentAndPropertyIsChanged()
        {
            // Arrange
            var opt = new DbContextOptionsBuilder<JourneyContext>()
                .UseInMemoryDatabase(databaseName: "JourneysDB")
                .Options;

            using (var context = new JourneyContext(opt))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Add(GetSingleStationSample());
                context.SaveChanges();
            }

            // Act
            using (var context = new JourneyContext(opt))
            {
                Guid statiId = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00");
                var controller = new StationsController(context);
                var res = await controller.PutStation(statiId,
                    new Station
                    {
                        StationId = statiId,
                        AddressFinnish = "Kivikatu 2",
                        AddressSwedish = "Stengata",
                        Capacity = 7,
                        CityFinnish = "Espoo",
                        CitySwedish = "Esbo",
                        IdInt = 1,
                        LocationX = 24.840319m,
                        LocationY = 60.165826m,
                        NameEnglish = "Big station",
                        NameFinnish = "Iso asema",
                        NameSwedish = "Stora stationen",
                        Operator = "Operator 1"
                    });
                var station = context.Stations.Find(statiId);

                // Assert
                Assert.NotNull(res);
                Assert.NotNull(station);
                Assert.IsType<NoContentResult>(res);
                Assert.Equal("Kivikatu 2", station.AddressFinnish);
            }
        }

        [Fact]
        public async Task PutStation_NotSameStationId_ShouldReturnBadRequest()
        {
            // Arrange
            var opt = new DbContextOptionsBuilder<JourneyContext>()
                .UseInMemoryDatabase(databaseName: "JourneysDB")
                .Options;

            using (var context = new JourneyContext(opt))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Add(GetSingleStationSample());
                context.SaveChanges();
            }

            // Act
            using (var context = new JourneyContext(opt))
            {
                Guid statiId = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00");
                var controller = new StationsController(context);
                var res = await controller.PutStation(new Guid("22223344-5566-7788-99AA-BBCCDDEEFF00"),
                    new Station
                    {
                        StationId = statiId,
                        AddressFinnish = "Kivikatu",
                        AddressSwedish = "Stengata",
                        Capacity = 7,
                        CityFinnish = "Espoo",
                        CitySwedish = "Esbo",
                        IdInt = 1,
                        LocationX = 24.840319m,
                        LocationY = 60.165826m,
                        NameEnglish = "Big station",
                        NameFinnish = "Iso asema",
                        NameSwedish = "Stora stationen",
                        Operator = "Operator 1"
                    });

                // Assert
                Assert.NotNull(res);
                Assert.IsType<BadRequestResult>(res);
            }
        }

        [Fact]
        public async Task DeleteStation_ShouldReturnNoContent()
        {
            // Arrange
            var opt = new DbContextOptionsBuilder<JourneyContext>()
                .UseInMemoryDatabase(databaseName: "JourneysDB")
                .Options;

            using (var context = new JourneyContext(opt))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Add(GetSingleStationSample());
                context.SaveChanges();
            }

            // Act
            using (var context = new JourneyContext(opt))
            {
                var controller = new StationsController(context);
                var res = (await controller.DeleteStation(new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00")));

                // Assert
                Assert.NotNull(res);
                Assert.IsType<NoContentResult>(res);
            }
        }

        [Fact]
        public async Task DeleteStation_StationDoesntExist_ShouldReturnNotFound()
        {
            // Arrange
            var opt = new DbContextOptionsBuilder<JourneyContext>()
                .UseInMemoryDatabase(databaseName: "JourneysDB")
                .Options;

            using (var context = new JourneyContext(opt))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Add(GetSingleStationSample());
                context.SaveChanges();
            }

            // Act
            using (var context = new JourneyContext(opt))
            {
                var controller = new StationsController(context);
                var res = (await controller.DeleteStation(new Guid("22223344-5566-7788-99AA-BBCCDDEEFF00")));

                // Assert
                Assert.NotNull(res);
                Assert.IsType<NotFoundResult>(res);
            }
        }

        /// <summary>
        /// Returns a single station sample.
        /// </summary>
        /// <returns>One station sample.</returns>
        private Station GetSingleStationSample()
        {
            var station = new Station
            {
                StationId = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00"),
                AddressFinnish = "Kivikatu",
                AddressSwedish = "Stengata",
                Capacity = 7,
                CityFinnish = "Espoo",
                CitySwedish = "Esbo",
                IdInt = 1,
                LocationX = 24.840319m,
                LocationY = 60.165826m,
                NameEnglish = "Big station",
                NameFinnish = "Iso asema",
                NameSwedish = "Stora stationen",
                Operator = "Operator 1"
            };
            return station;
        }

        /// <summary>
        /// Returns a single station sample (without assigned Guid).
        /// </summary>
        /// <returns>One station sample.</returns>
        private StationDto GetSingleStationDtoSample()
        {
            var station = new StationDto
            {
                AddressFinnish = "Kivikatu",
                AddressSwedish = "Stengata",
                Capacity = 7,
                CityFinnish = "Espoo",
                CitySwedish = "Esbo",
                IdInt = 1,
                LocationX = 24.840319m,
                LocationY = 60.165826m,
                NameEnglish = "Big station",
                NameFinnish = "Iso asema",
                NameSwedish = "Stora stationen",
                Operator = "Operator 1"
            };
            return station;
        }

        /// <summary>
        /// Returns a sample of stations.
        /// </summary>
        /// <returns>A sample of five stations.</returns>
        private List<Station> GetStationsSample()
        {
            List<Station> stationsSample = new List<Station>();
            //  year, month, day, hour, minute, and second.
            stationsSample.Add(new Station
            {
                StationId = Guid.NewGuid(),
                AddressFinnish = "Kivikatu",
                AddressSwedish = "Stengata",
                Capacity = 7,
                CityFinnish = "Espoo",
                CitySwedish = "Esbo",
                IdInt = 1,
                LocationX = 24.840319m,
                LocationY = 60.165826m,
                NameEnglish = "Big station",
                NameFinnish = "Iso asema",
                NameSwedish = "Stora stationen",
                Operator = "Operator 1"
            });
            stationsSample.Add(new Station
            {
                StationId = Guid.NewGuid(),
                AddressFinnish = "Kivikatu 1",
                AddressSwedish = "Stengata 1",
                Capacity = 10,
                CityFinnish = "Espoo",
                CitySwedish = "Esbo",
                IdInt = 2,
                LocationX = 24.840319m,
                LocationY = 60.165815m,
                NameEnglish = "Big station",
                NameFinnish = "Iso asema",
                NameSwedish = "Stora stationen",
                Operator = "Operator 1"
            });
            stationsSample.Add(new Station
            {
                StationId = Guid.NewGuid(),
                AddressFinnish = "Kivikatu 2",
                AddressSwedish = "Stengata 2",
                Capacity = 1,
                CityFinnish = "Espoo",
                CitySwedish = "Esbo",
                IdInt = 3,
                LocationX = 24.840321m,
                LocationY = 60.16582m,
                NameEnglish = "Big station",
                NameFinnish = "Iso asema",
                NameSwedish = "Stora stationen",
                Operator = "Operator 1"
            });
            stationsSample.Add(new Station
            {
                StationId = Guid.NewGuid(),
                AddressFinnish = "Kivikatu 3",
                AddressSwedish = "Stengata 3",
                Capacity = 25,
                CityFinnish = "Espoo",
                CitySwedish = "Esbo",
                IdInt = 4,
                LocationX = 24.840299m,
                LocationY = 60.165852m,
                NameEnglish = "Big station",
                NameFinnish = "Iso asema",
                NameSwedish = "Stora stationen",
                Operator = "Operator 1"
            });
            stationsSample.Add(new Station
            {
                StationId = Guid.NewGuid(),
                AddressFinnish = "Kivikatu 4",
                AddressSwedish = "Stengata 4",
                Capacity = 9,
                CityFinnish = "Espoo",
                CitySwedish = "Esbo",
                IdInt = 5,
                LocationX = 24.840309m,
                LocationY = 60.165841m,
                NameEnglish = "Big station",
                NameFinnish = "Iso asema",
                NameSwedish = "Stora stationen",
                Operator = "Operator 1"
            });

            return stationsSample;
        }

        /// <summary>
        /// Returns a sample of journeys.
        /// </summary>
        /// <returns>A sample of five journeys.</returns>
        private List<Journey> GetJourneysSample()
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
                ReturnStationId = 1,
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
                ReturnStationId = 1,
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
                ReturnStationId = 1,
                CoveredDistance = 790,
                Duration = 65
            });

            return journeysSample;
        }
    }


}
