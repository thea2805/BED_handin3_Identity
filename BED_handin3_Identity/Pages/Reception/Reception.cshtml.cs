using BED_handin3_Identity.Data;
using BED_handin3_Identity.Hub;
using BED_handin3_Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace BED_handin3_Identity.Pages.Reception
{
    [Authorize("CanEnterReception")]
    public class ReceptionModel : PageModel
    {
        // Add the signalR Hub thing here and in constructor
        private readonly IHubContext<UpdaterHub, IUpdaterHub> _updaterHubContext;
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
        public List<SelectListItem> SelectRooms { get; set; }


        // Bind properties here (for the list of checked in guests)
        [BindProperty] public DateTime TodayDate { get; set; } = DateTime.Today;
        public List<Guest> GuestList { get; set; } = new List<Guest>();
        public List<Room> RoomList { get; set; } = new List<Room>();


        // Constructor here 
        public ReceptionModel(ApplicationDbContext context, IHubContext<UpdaterHub, IUpdaterHub> updaterHubContext)
        {
            _context = context;
            _updaterHubContext = updaterHubContext;
        }


        public async Task<IActionResult> OnGetAsync()
        {
            // Find all the booked rooms for the selected date (today)
            // Find all rooms
            // Remove all the booked rooms from the list of all rooms
            // The rooms will be displayed as available rooms in a drop down menu

            var bookedRooms = await _context.Bookings.Where(b => b.BookingDate == ReceptionDate).Include(b => b.Room)
                .Select(b => new SelectListItem
                {
                    Value = b.Room.RoomId.ToString(),
                    Text = b.Room.RoomNumber.ToString(),
                }).ToListAsync();

            var allRooms = await _context.Rooms.Select(r => new SelectListItem
            {
                Value = r.RoomId.ToString(),
                Text = r.RoomNumber.ToString(),
            }).ToListAsync();

            var removed = allRooms.RemoveAll(a => bookedRooms.Any(r => a.Value == r.Value));
            SelectRooms = allRooms;

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

        public async Task<IActionResult> OnPostDateAsync()
        {
            // This does the same as the onGet, but with the selected date (any date)
            // Find all the booked rooms for the selected date (today)
            // Find all rooms
            // Remove all the booked rooms from the list of all rooms
            // The rooms will be displayed as available rooms in a drop down menu

            var bookedRooms = await _context.Bookings.Where(b => b.BookingDate == ReceptionDate).Include(b => b.Room)
                .Select(b => new SelectListItem
                {
                    Value = b.Room.RoomId.ToString(),
                    Text = b.Room.RoomNumber.ToString(),
                }).ToListAsync();

            var allRooms = await _context.Rooms.Select(r => new SelectListItem
            {
                Value = r.RoomId.ToString(),
                Text = r.RoomNumber.ToString(),
            }).ToListAsync();

            var removed = allRooms.RemoveAll(a => bookedRooms.Any(r => a.Value == r.Value));
            SelectRooms = allRooms;

            return Page();
        }

        public async Task<IActionResult> OnPostReceptionAsync()
        {
            // Creates a new Booking
            // Add the selected number of adults + children -> add roomNumber
            // Create new Booking with the Guests, RoomNumber & Date

            // Add to database


            if (RoomExists(RoomNumber) && (Adults != 0 || Children != 0))
            {
                var GuestList = new List<Guest>();
                for (int i = 0; i < Children; i++)
                {
                    GuestList.Add(new Guest()
                    {
                        IsAdult = false,
                        RoomId = RoomNumber
                    });
                }

                for (int i = 0; i < Adults; (i)++)
                {
                    GuestList.Add(new Guest()
                    {
                        IsAdult = true,
                        RoomId = RoomNumber
                    });
                }

                var newBooking = new Booking()
                {
                    Guests = GuestList,
                    RoomId = RoomNumber,
                    BookingDate = ReceptionDate
                };
                _context.Bookings.Add(newBooking);
                await _context.SaveChangesAsync();

                await _updaterHubContext.Clients.All.UpdatePage("UpdatePage");

                return Page();
            }
            else
            {
                return BadRequest();
            }
            
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.RoomId == id);
        }
    }
}
