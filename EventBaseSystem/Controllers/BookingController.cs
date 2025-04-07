using EventBaseSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventBaseSystem.Controllers
{
    public class BookingController : Controller
    {
        
        

            private readonly EventBaseDBContext _context;
            public BookingController(EventBaseDBContext context)
            {
                _context = context;
            }
            public async Task<IActionResult> Index()

            {
                var Booking = await _context.Booking
                    .Include(i => i.Event)
                    .Include(i => i.Venue)
                    .ToListAsync();

                return View(Booking);
            }
        public IActionResult Create()
        {
            ViewBag.EventName = _context.Event.ToList();
            ViewBag.Venue = _context.Venue.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(booking booking)
        {
            if (ModelState.IsValid)
            {

                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Event = _context.Event.ToList();
            ViewBag.Venue = _context.Venue.ToList();
            return View(booking);

        }
    }
}
