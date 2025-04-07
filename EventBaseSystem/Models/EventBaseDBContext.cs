using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EventBaseSystem.Models
{
    public class EventBaseDBContext : DbContext
    {
        public EventBaseDBContext(DbContextOptions<EventBaseDBContext> options) : base(options)
        {

        }
            public DbSet<Venue> Venue { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<booking> Booking { get; set; }
    }
}
        

    

