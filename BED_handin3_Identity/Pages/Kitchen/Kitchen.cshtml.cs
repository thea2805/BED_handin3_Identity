using BED_handin3_Identity.Data;
using BED_handin3_Identity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;

namespace BED_handin3_Identity.Pages.Kitchen
{
    public class KitchenModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        // Constructor
        public KitchenModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // Bind properties here (for the booking)
        [BindProperty] public DateTime ChosenDate { get; set; } = DateTime.Today;
        public List<Guest> GuestList { get; set; } = new List<Guest>();


        public async Task<IActionResult> OnGetAsync()
        {
            // Find all booked guests for the date 
            var bookingList = await _context.Bookings.Where(b => b.BookingDate == ChosenDate)
                .Include(b => b.Guests).ToListAsync();

            foreach (var booking in bookingList)
            {
                GuestList.AddRange(booking.Guests);
            }
            return Page();
        }


        // This method is called, when the date is updated
        public async Task<IActionResult> OnPostAsync()
        {
            // Find all booked guests for the date 
            var bookingList = await _context.Bookings.Where(b => b.BookingDate == ChosenDate)
                .Include(b => b.Guests).ToListAsync();

            foreach (var booking in bookingList)
            {
                GuestList.AddRange(booking.Guests);
            }
            return Page();
        }
    }
}
