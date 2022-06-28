using BED_handin3_Identity.Data;
using BED_handin3_Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BED_handin3_Identity.Pages.Reception
{
    [Authorize("CanEnterReception")]
    public class ReceptionListModel : PageModel
    {

        // Bind properties here (for the list of checked in guests)
        [BindProperty] public DateTime TodayDate { get; set; } = DateTime.Today;
        public List<Guest> GuestList { get; set; } = new List<Guest>();
        public List<Room> RoomList { get; set; } = new List<Room>();

        private readonly ApplicationDbContext _context;

        // Constructor here 
        public ReceptionListModel(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public async Task<IActionResult> OnGetAsync()
        {

            // Show a list of all checked in guests + roomNumber
            // Find all bookings with the current date
            // Find the attached guests with rooms 
            // Add Guests to guestlist + rooms to roomlist

            var bookingList = await _context.Bookings.Where(b => b.BookingDate == TodayDate).Include(b => b.Guests)
                .Include(b => b.Room).ToListAsync();
            foreach (var booking in bookingList)
            {
                var guests = booking.Guests.Where(g => g.IsCheckedIn == true);
                RoomList.Add(booking.Room);
                GuestList.AddRange(guests);
            }

            return Page();
        }
    }
}
