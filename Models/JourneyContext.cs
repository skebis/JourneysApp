using Microsoft.EntityFrameworkCore;
using solita_assignment.Classes;

namespace solita_assignment.Models
{
    public class JourneyContext : DbContext
    {
        public DbSet<Journey> Journeys { get; set; }
        public DbSet<Station> Stations { get; set; }

        public string DbPath { get; }

        public JourneyContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "BikeJourneys.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) 
        { 
            options.UseSqlite($"Data Source={DbPath}");
        }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Journey>().HasData(new Journey()
            { 
                CoveredDistance = 1, Departure = DateTime.Now, DepartureStationId = 0, DepartureStationName = "a",
                Return = DateTime.Now, Duration = 2, ReturnStationId = 0, ReturnStationName = "b", JourneyId = Guid.NewGuid()
            });
        }*/
    }
}
