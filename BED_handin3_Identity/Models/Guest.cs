namespace BED_handin3_Identity.Models
{
    public class Guest
    {
        public int GuestId { get; set; }
        public bool IsAdult { get; set; }

        public bool IsCheckedIn { get; set; }

        public int RoomId { get; set; }
        public Room? Room { get; set; }
    }
}
