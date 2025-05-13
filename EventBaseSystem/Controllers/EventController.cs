using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventBaseSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

//methods taken from Juliana Adeola.
namespace EventBaseSystem.Controllers
{
    public class EventController : Controller
    {
        private readonly EventBaseDBContext _context;
        public EventController(EventBaseDBContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()

        {
            var Event = await _context.Event
                .Include(e => e.Venue)
                .ToListAsync();

            return View(Event);
        }

        public IActionResult Create()
        {

            ViewData["VenueID"] = new SelectList(_context.Venue, "VenueID", "Name");
            return View();
        }
        

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Event = await _context.Event.FindAsync(id);
            if (Event == null) return NotFound();

            bool hasbookings = await _context.Booking.AnyAsync(b => b.EventID == id);
            if (hasbookings)
            {
                TempData["ErrorMessage"] = "Cannot delete event with existing bookings.";
                return RedirectToAction(nameof(Index));
            }
            _context.Event.Remove(Event);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Event deleted successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event newEvent)
        {


            if (ModelState.IsValid)
            {
                newEvent.CreatedAt = DateTime.Now;

                _context.Add(newEvent);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Event created successfully.";
                return RedirectToAction(nameof(Index));

            }
                ViewData["VenueID"] = new SelectList(_context.Venue, "VenueID", "Name");
                return View(newEvent);
            
           

        }

    }
}
<<<<<<< HEAD
=======
    
>>>>>>> 8677436 (Cleaned up unnecessary files and added .gitignore)
