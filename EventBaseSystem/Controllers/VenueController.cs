using EventBaseSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Venue venue)
        {
            

            if (!ModelState.IsValid)
            {
               
            
            venue.CreatedAt = DateTime.Now;

            if (venue.Image != null)
            {
                var blobUrl = await UploadImageToBlobAsync(venue.Image);
                venue.ImageURL = blobUrl;
            }

            _context.Add(venue);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Venue created successfully.";
            return RedirectToAction(nameof(Index));
        }
         return View(venue);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Venue = await _context.Venue.FindAsync(id);
            if (Venue == null)
            {
                return NotFound();
            }
            var hasbookings = await _context.Booking.AnyAsync(b => b.VenueID == id);
            if (hasbookings)
            {
                TempData["ErrorMessage"] = "Cannot delete venue with existing bookings.";
                return RedirectToAction(nameof(Index));
            }
            _context.Venue.Remove(Venue);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Venue deleted successfully.";
            return RedirectToAction(nameof(Index));
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
            if(id == null)
            {
                return NotFound();
            }

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
        [ValidateAntiForgeryToken]
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
                    if (venue.Image != null)
                    {
                        var blobUrl = await UploadImageToBlobAsync(venue.Image);
                        venue.ImageURL = blobUrl;
                    }
                    else
                    {

                    }
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

        private async Task<string> UploadImageToBlobAsync(IFormFile imageFile)
        {
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=part2poe;AccountKey=FnkbYw0VfzWSQUhDbt/4z/O3VZP1Sw6+PGgXafEenjx55v4Em/OZlQPyJq/scLPxsxj2g1gq6Qoe+AStPRm/Hw==;EndpointSuffix=core.windows.net";
            var containerName = "images";

            var blobServiceClient = new Azure.Storage.Blobs.BlobServiceClient(connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(Guid.NewGuid() + Path.GetExtension(imageFile.FileName));

            var blobHttpHeaders = new Azure.Storage.Blobs.Models.BlobHttpHeaders
            {
                ContentType = imageFile.ContentType
            };

            using (var stream = imageFile.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, new Azure.Storage.Blobs.Models.BlobUploadOptions
                {
                    HttpHeaders = blobHttpHeaders
                });
            }

            return blobClient.Uri.ToString();
        }
    }
}
    

