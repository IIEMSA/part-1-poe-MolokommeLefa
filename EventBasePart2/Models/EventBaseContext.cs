using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace EventBasePart2.Models
{
    public partial class EventBaseContext : DbContext
    {
        public EventBaseContext()
            : base("name=EventBaseContext")
        {
        }

        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Venue> Venues { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Bookings)
                .WithOptional(e => e.Event)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Venue>()
                .HasMany(e => e.Bookings)
                .WithOptional(e => e.Venue)
                .WillCascadeOnDelete();
        }
    }
}
