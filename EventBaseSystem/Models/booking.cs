using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EventBaseSystem.Models
{
    public class booking
    {
        [Key]
    public int BookingID { get; set; }
        [Required(ErrorMessage = "venue is required")]
        [ForeignKey("Venue")]
        public int VenueID { get; set; }
        public Venue? Venue { get; set; }

        [ForeignKey("Event")]
        [Required(ErrorMessage = "event is required")]
        public int EventID { get; set; }
        public Event? Event { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string? CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
