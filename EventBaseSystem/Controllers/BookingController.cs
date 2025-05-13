using EventBaseSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// methods taken from Juliana Adeola.
namespace EventBaseSystem.Controllers
{
    public class BookingController : Controller
    {
        
        

            private readonly EventBaseDBContext _context;
            public BookingController(EventBaseDBContext context)
            {
                _context = context;
            }
            public async Task<IActionResult> Index(string searchString)

            {
                var Booking =  _context.Booking
                    .Include(i => i.Event)
                    .Include(i => i.Venue)
                    .AsQueryable();

            if(!string.IsNullOrEmpty(searchString))
            {
                Booking = Booking.Where(b => b.Venue.Name.Contains(searchString) || b.Event.EventName.Contains(searchString));
            }
        

                return View(await Booking.ToListAsync());


            }
        public IActionResult Create()
        {
            ViewBag.EventName = _context.Event.ToList();
            ViewBag.Venue = _context.Venue.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(booking booking)
        {
            // always repopulate for *any* exit path that returns the view
            booking.CreatedAt = DateTime.Now;


            // 1) Event exists?
            var selectedEvent = await _context.Event.FindAsync(booking.EventID);
            if (selectedEvent == null)
            {
                ModelState.AddModelError("", "Selected event not found.");
            }
            else
            {
                // 2) Double-booking check
                bool conflict = await _context.Booking.AnyAsync(b =>
                    b.VenueID == booking.VenueID &&
                    b.StartDate <= booking.EndDate &&
                    b.EndDate >= booking.StartDate);

                if (conflict)
                    ModelState.AddModelError("", "This venue is already booked for that date range.");
            }

            // 3) If all good, save
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Booking created successfully.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.EventName = _context.Event.ToList();
            ViewBag.Venue = _context.Venue.ToList();
            return View(booking);
        }


    }
}

