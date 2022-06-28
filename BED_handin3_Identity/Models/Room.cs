namespace BED_handin3_Identity.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public int RoomNumber { get; set; }

        public List<Booking>? ListOfBookings { get; set; }
    }
}
