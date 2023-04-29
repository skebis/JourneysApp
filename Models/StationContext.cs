using Microsoft.EntityFrameworkCore;

namespace solita_assignment.Models
{
    public class StationContext : DbContext
    {
        public StationContext(DbContextOptions<StationContext> options) : base(options)
        {

        }
        public DbSet<Station> Stations { get; set; } = null!;
    }
}
