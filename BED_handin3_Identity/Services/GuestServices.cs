using BED_handin3_Identity.Models;

namespace BED_handin3_Identity.Services
{
    public class GuestServices
    {
        public GuestServices()
        {
            bookingDate = DateTime.Today;
        }

        public DateTime bookingDate { get; set; }
        public List<Guest> Guests { get; set; } = new List<Guest>();

    }
}
