using BED_handin3_Identity.Data;
using BED_handin3_Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BED_handin3_Identity.Pages.Restaurant
{
    [Authorize("CanEnterRestaurant")]
    public class RestaurantModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RestaurantModel(ApplicationDbContext context)
        {
            _context = context;
        }


        // Bind properties here 
        [BindProperty] public int Adults { get; set; } = 0;
        [BindProperty] public int Children { get; set; } = 0;
        [BindProperty] public int SelectedRoomNumber { get; set; } = 0;

        [BindProperty]
        public DateTime TodayDate { get; set; } = DateTime.Today;
        public List<SelectListItem> SelectRooms { get; set; }

        public Booking BreakfastBooking { get; set; } = new Booking();


        public async Task<IActionResult> OnGetAsync()
        {
            SelectRooms = await _context.Bookings.Where(b => b.BookingDate == DateTime.Today).Include(b => b.Room)
                .Select(b => new SelectListItem
                {
                    Value = b.Room.RoomId.ToString(),
                    Text = b.Room.RoomNumber.ToString(),
                }).ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostRoomAsync()
        {

            SelectRooms = await _context.Bookings.Where(b => b.BookingDate == DateTime.Today)
                .Include(b => b.Room)
                .Select(b => new SelectListItem
                {
                    Value = b.Room.RoomId.ToString(),
                    Text = b.Room.RoomNumber.ToString(),
                }).ToListAsync();

            BreakfastBooking = await _context.Bookings
                .Where(b => b.BookingDate == DateTime.Today)
                .Where(b => b.Room.RoomNumber == SelectedRoomNumber)
                .Include(b => b.Guests).FirstOrDefaultAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostCheckinAsync()
        {
            BreakfastBooking = await _context.Bookings
                .Where(b => b.BookingDate == DateTime.Today)
                .Where(b => b.Room.RoomNumber == SelectedRoomNumber)
                .Include(b => b.Guests).FirstOrDefaultAsync();

            for (int i = 0; i < Adults; i++)
            {
                var adultGuest = BreakfastBooking.Guests.Where(g => g.IsAdult && !g.IsCheckedIn).FirstOrDefault();
                adultGuest.IsCheckedIn = true;
                _context.Attach(adultGuest).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }

            for (int i = 0; i < Adults; i++)
            {
                var childGuest = BreakfastBooking.Guests.Where(g => !g.IsAdult && !g.IsCheckedIn).FirstOrDefault();
                childGuest.IsCheckedIn = true;
                _context.Attach(childGuest).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }

            return Page();
        }

    }
}
