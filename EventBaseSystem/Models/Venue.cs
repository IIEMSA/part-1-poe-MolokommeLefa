using System.ComponentModel.DataAnnotations;
using EventBaseSystem.Models;

namespace EventBaseSystem.Models
{
    public class Venue
    {
        [Key]
        public int VenueID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public int Capacity { get; set; }

        public string ImageURL { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<EventBaseSystem.Models.booking> Bookings { get; set; } = new();
    }
}

