using Microsoft.EntityFrameworkCore;
using solita_assignment.Classes;

namespace solita_assignment.Models
{
    public class JourneyContext : DbContext
    {
        public virtual DbSet<Journey> Journeys { get; set; }
        public virtual DbSet<Station> Stations { get; set; }

        public string DbPath { get; }

        public static readonly string dbFileName = "BikeJourneys.db";

        public JourneyContext(DbContextOptions<JourneyContext> opt) : base(opt)
        {
            // Create SQLite database to solution root directory.
            DbPath = Path.Join(Environment.CurrentDirectory, dbFileName);
        }

        /*protected override void OnConfiguring(DbContextOptionsBuilder options) 
        { 
            options.UseSqlite($"Data Source={DbPath}");
        }*/
    }
}
