namespace BED_handin3_Identity.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.Now.Date;
        public List<Guest>? Guests { get; set; } = new List<Guest>();
        public int RoomId { get; set; }
        public Room? Room { get; set; }
    }
}
