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
    public class TestJourneysController
    {

        [Fact]
        public async Task GetJourneysPagination_ShouldReturnGivenAmount()
        {
            // Arrange
            var opt = new DbContextOptionsBuilder<JourneyContext>()
                .UseInMemoryDatabase(databaseName: "JourneysDB")
                .Options;

            using (var context = new JourneyContext(opt))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.AddRange(GetJourneysSample());
                context.SaveChanges();
            }

            // Act
            using (var context = new JourneyContext(opt))
            {
                var controller = new JourneysController(context);
                var pagination = new Pagination { Page = 1, PageSize = 5 };
                var res = (await controller.GetJourneys(pagination)).Value;

                // Assert
                Assert.NotNull(res);
                Assert.Equal(5, res.Count());
            }
        }

        [Fact]
        public async Task GetJourneysPagination_PageSizeLessThanDatabaseEntries_ShouldReturnPageSizeAmount()
        {
            // Arrange
            var opt = new DbContextOptionsBuilder<JourneyContext>()
                .UseInMemoryDatabase(databaseName: "JourneysDB")
                .Options;

            using (var context = new JourneyContext(opt))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.AddRange(GetJourneysSample());
                context.SaveChanges();
            }

            // Act
            using (var context = new JourneyContext(opt))
            {
                var controller = new JourneysController(context);
                var pagination = new Pagination { Page = 1, PageSize = 2 };
                var res = (await controller.GetJourneys(pagination)).Value;

                // Assert
                Assert.NotNull(res);
                Assert.Equal(2, res.Count());
            }
        }

        [Fact]
        public async Task GetJourneysPagination_PageLessThanOne_ShouldReturnBadRequest()
        {
            // Arrange
            var opt = new DbContextOptionsBuilder<JourneyContext>()
                .UseInMemoryDatabase(databaseName: "JourneysDB")
                .Options;

            using (var context = new JourneyContext(opt))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.AddRange(GetJourneysSample());
                context.SaveChanges();
            }

            // Act
            using (var context = new JourneyContext(opt))
            {
                var controller = new JourneysController(context);
                var pagination = new Pagination { Page = -1, PageSize = 5 };
                var res = (await controller.GetJourneys(pagination)).Result;

                // Assert
                Assert.NotNull(res);
                Assert.IsType<BadRequestResult>(res);
            }
        }

        [Fact]
        public async Task GetJourneysPagination_PageSizeLessThanOne_ShouldReturnBadRequest()
        {
            // Arrange
            var opt = new DbContextOptionsBuilder<JourneyContext>()
                .UseInMemoryDatabase(databaseName: "JourneysDB")
                .Options;

            using (var context = new JourneyContext(opt))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.AddRange(GetJourneysSample());
                context.SaveChanges();
            }

            // Act
            using (var context = new JourneyContext(opt))
            {
                var controller = new JourneysController(context);
                var pagination = new Pagination { Page = 1, PageSize = -5 };
                var res = (await controller.GetJourneys(pagination)).Result;

                // Assert
                Assert.NotNull(res);
                Assert.IsType<BadRequestResult>(res);
            }
        }

        [Fact]
        public async Task GetJourney_ShouldReturnRequestedJourney()
        {
            // Arrange
            var opt = new DbContextOptionsBuilder<JourneyContext>()
                .UseInMemoryDatabase(databaseName: "JourneysDB")
                .Options;

            using (var context = new JourneyContext(opt))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Add(GetSingleJourneySample());
                context.SaveChanges();
            }

            // Act
            using (var context = new JourneyContext(opt))
            {
                var controller = new JourneysController(context);
                var res = (await controller.GetJourney(new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00"))).Value;

                // Assert
                Assert.NotNull(res);
                Assert.Equal(res.JourneyId, new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00"));
            }
        }

        [Fact]
        public async Task GetJourney_JourneyDoesntExist_ShouldReturnNotFound()
        {
            // Arrange
            var opt = new DbContextOptionsBuilder<JourneyContext>()
                .UseInMemoryDatabase(databaseName: "JourneysDB")
                .Options;

            using (var context = new JourneyContext(opt))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Add(GetSingleJourneySample());
                context.SaveChanges();
            }

            // Act
            using (var context = new JourneyContext(opt))
            {
                var controller = new JourneysController(context);
                var res = (await controller.GetJourney(new Guid("22223344-5566-7788-99AA-BBCCDDEEFF00"))).Result;

                // Assert
                Assert.NotNull(res);
                Assert.IsType<NotFoundResult>(res);
            }
        }

        [Fact]
        public async Task PostJourney_ShouldReturnJourneyCreated()
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
                var controller = new JourneysController(context);
                var res = (await controller.PostJourney(GetSingleJourneyDtoSample())).Result;

                // Assert
                Assert.NotNull(res);
                Assert.IsType<CreatedAtActionResult>(res);
            }
        }

        [Fact]
        public async Task PutJourney_ValidJourneyModel_ShouldReturnNoContentAndPropertyIsChanged()
        {
            // Arrange
            var opt = new DbContextOptionsBuilder<JourneyContext>()
                .UseInMemoryDatabase(databaseName: "JourneysDB")
                .Options;

            using (var context = new JourneyContext(opt))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Add(GetSingleJourneySample());
                context.SaveChanges();
            }

            // Act
            using (var context = new JourneyContext(opt))
            {
                Guid journId = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00");
                var controller = new JourneysController(context);
                var res = await controller.PutJourney(journId,
                    new Journey
                    {
                        JourneyId = journId,
                        Departure = new DateTime(2023, 8, 22, 10, 58, 10),
                        Return = new DateTime(2023, 8, 22, 12, 45, 2),
                        DepartureStationName = "Station",
                        DepartureStationId = 1,
                        ReturnStationName = "Station 2",
                        ReturnStationId = 2,
                        CoveredDistance = 3000,
                        Duration = 1200
                    });
                var journey = context.Journeys.Find(journId);

                // Assert
                Assert.NotNull(res);
                Assert.NotNull(journey);
                Assert.IsType<NoContentResult>(res);
                Assert.Equal(3000, journey.CoveredDistance);
            }
        }

        [Fact]
        public async Task PutJourney_NotSameJourneyId_ShouldReturnBadRequest()
        {
            // Arrange
            var opt = new DbContextOptionsBuilder<JourneyContext>()
                .UseInMemoryDatabase(databaseName: "JourneysDB")
                .Options;

            using (var context = new JourneyContext(opt))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Add(GetSingleJourneySample());
                context.SaveChanges();
            }

            // Act
            using (var context = new JourneyContext(opt))
            {
                Guid journId = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00");
                var controller = new JourneysController(context);
                var res = await controller.PutJourney(new Guid("22223344-5566-7788-99AA-BBCCDDEEFF00"),
                    new Journey
                    {
                        JourneyId = journId,
                        Departure = new DateTime(2023, 8, 22, 10, 58, 10),
                        Return = new DateTime(2023, 8, 22, 12, 45, 2),
                        DepartureStationName = "Station",
                        DepartureStationId = 1,
                        ReturnStationName = "Station 2",
                        ReturnStationId = 2,
                        CoveredDistance = 3000,
                        Duration = 1200
                    });

                // Assert
                Assert.NotNull(res);
                Assert.IsType<BadRequestResult>(res);
            }
        }

        /// <summary>
        /// Returns a single journey sample.
        /// </summary>
        /// <returns>One journey sample.</returns>
        private Journey GetSingleJourneySample()
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

        /// <summary>
        /// Returns a single journey sample (without assigned Guid).
        /// </summary>
        /// <returns>One journey sample.</returns>
        private JourneyDto GetSingleJourneyDtoSample()
        {
            var journey = new JourneyDto
            {
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
