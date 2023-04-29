using Microsoft.EntityFrameworkCore;

namespace solita_assignment.Models
{
    public class JourneyContext : DbContext
    {
        public JourneyContext(DbContextOptions<JourneyContext> options) : base(options)
        {

        }
        public DbSet<Journey> Journeys { get; set; } = null!;
    }
}
