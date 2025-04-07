using EventBaseSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
//methods taken from - Juliana Adeola.
namespace EventBaseSystem.Controllers
{
    public class VenueController : Controller
    {
        
        private readonly EventBaseDBContext _context;
        public VenueController(EventBaseDBContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()

        {
            var Venue = await _context.Venue.ToListAsync();

            return View(Venue);
        
        ;
    }
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(Venue venue)
        {
            if (ModelState.IsValid)
            {

                _context.Add(venue);
                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(Index));
            }


            return View(venue);
        }
        public async Task<IActionResult> Details(int? id)
        {

            var Venue = await _context.Venue.FirstOrDefaultAsync(m => m.VenueID == id);

            if (Venue == null)
            {
                return NotFound();
            }
            return View(Venue);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var Venue = await _context.Venue.FirstOrDefaultAsync(m => m.VenueID == id);


            if (Venue == null)
            {
                return NotFound();
            }
            return View(Venue);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var Venue = await _context.Venue.FindAsync(id);
            _context.Venue.Remove(Venue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool VenueExists(int id)
        {
            return _context.Venue.Any(e => e.VenueID == id);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Venue = await _context.Venue.FindAsync(id);
            if (id == null)
            {
                return NotFound();
            }

            return View(Venue);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Venue venue)
        {
            if (id != venue.VenueID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VenueExists(venue.VenueID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(venue);
        }


    }
}
    

