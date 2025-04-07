using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventBaseSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


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
            var Event = await _context.Event.ToListAsync();
            return View(Event);
        }

        public IActionResult Create()
        {
            return View();
            ViewData["VenueID"] = new SelectList(_context.Venue, "VenueID", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Event Event)
        {


            if (ModelState.IsValid)
            {

                _context.Add(Event);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
                ViewData["VenueID"] = new SelectList(await _context.Venue.ToListAsync(), "VenueID", "Name", Event.VenueID);

            }

            return View(Event);
        }

    }
}