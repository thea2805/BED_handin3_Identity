using BED_handin3_Identity.Data;
using BED_handin3_Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BED_handin3_Identity.Pages.Reception
{
    [Authorize("CanEnterReception")]
    public class ReceptionModel : PageModel
    {
        // Add the signalR Hub thing here and in constructor
        private readonly ApplicationDbContext _context;


        // Bind properties here (for the booking)
        [BindProperty]
        public int Adults { get; set; }
        [BindProperty]
        public int Children { get; set; }
        [BindProperty]
        public DateTime ReceptionDate { get; set; } = DateTime.Today;
        [BindProperty]
        public int RoomNumber { get; set; }

        // Bind properties here (for the list of checked in guests)
        [BindProperty] public DateTime TodayDate { get; set; } = DateTime.Today;
        public List<Guest> GuestList { get; set; } = new List<Guest>();
        public List<Room> RoomList { get; set; } = new List<Room>();


        // Constructor here 
        public ReceptionModel(ApplicationDbContext context)
        {
            _context = context;
            //signalR
        }


        public async Task<IActionResult> OnGetAsync()
        {
            // Find all the booked rooms for the selected date (today)
            // Find all rooms
            // Remove all the booked rooms from the list of all rooms
            // The rooms will be displayed as available rooms in a drop down menu



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

        public async Task<IActionResult> OnPostAsync()
        {
            // This does the same as the onGet, but with the selected date (any date)
            return Page();
        }

        public async Task<IActionResult> OnPostReceptionAsync()
        {
            // Creates a new Booking
            // Add the selected number of adults + children -> add roomNumber
            // Create new Booking with the Guests, RoomNumber & Date

            // Add to database


            return Page();
        }
    }
}
