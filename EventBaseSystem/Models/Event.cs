using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace EventBaseSystem.Models
{
    public class Event
    {
        [Key]
        public int EventID { get; set; }

        [Required]
        public string EventName { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [ForeignKey("Venue")]
        public int VenueID { get; set; }
        public Venue Venue { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<EventBaseSystem.Models.booking> Bookings { get; set; } = new();
    }
}

