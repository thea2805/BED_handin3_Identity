using BED_handin3_Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BED_handin3_Identity.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Guest>? Guests { get; set; }
        public DbSet<Room>? Rooms { get; set; }
        public DbSet<Booking>? Bookings { get; set; }
    }
}