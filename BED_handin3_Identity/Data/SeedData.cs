using System.Security.Claims;
using BED_handin3_Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace BED_handin3_Identity.Data
{
    public class SeedData
    {
        public static void SeedDBData(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            if (!context.Rooms.Any())
            {
                SeedRooms(context);
                SeedBookings(context);
            }
        }


        private static void SeedRooms(ApplicationDbContext context)
        {
            context.Rooms.AddRange(
                new Room()
                {
                    RoomNumber = 1,
                },
                new Room()
                {
                    RoomNumber = 2,
                },
                new Room()
                {
                    RoomNumber = 3,
                }
            );
            context.SaveChanges();
        }

        private static void SeedBookings(ApplicationDbContext context)
        {
            context.Bookings.AddRange(
                new Booking()
                {
                    BookingDate = DateTime.Today,
                    RoomId = 1,
                    Guests = new List<Guest>
                    {
                    new Guest()
                    {
                        IsAdult = true,
                        RoomId = 1,
                    },
                    new Guest()
                    {
                        IsAdult = false,
                        RoomId = 1,
                    },
                    new Guest()
                    {
                        IsAdult = false,
                        RoomId = 1,
                    },
                    }
                },
                new Booking()
                {
                    BookingDate = DateTime.Today,
                    RoomId = 2,
                    Guests = new List<Guest>
                    {
                    new Guest()
                    {
                        IsAdult = true,
                        RoomId = 2,
                    },
                    new Guest()
                    {
                        IsAdult = true,
                        RoomId = 2,
                    },

                    }
                },
                new Booking()
                {
                    BookingDate = DateTime.Today,
                    RoomId = 3,
                    Guests = new List<Guest>
                    {
                    new Guest()
                    {
                        IsAdult = true,
                        RoomId = 3,
                    },
                    new Guest()
                    {
                        IsAdult = true,
                        RoomId = 3,
                    },
                    new Guest()
                    {
                        IsAdult = false,
                        RoomId = 3,
                    },
                    new Guest()
                    {
                        IsAdult = false,
                        RoomId = 3,
                    },
                    }
                }
            );

            context.SaveChanges();
        }


        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {

            // Seeding the receptionist 

            const string receptionistEmail = "receptionist@mail.dk";
            const string receptionistPassword = "password1";
            if (userManager == null)
                throw new ArgumentNullException(nameof(userManager));

            if (userManager.FindByNameAsync(receptionistEmail).Result == null)
            {
                var user = new IdentityUser();
                user.UserName = receptionistEmail;
                user.Email = receptionistEmail;
                user.EmailConfirmed = true;
                IdentityResult result = userManager.CreateAsync(user, receptionistPassword).Result;

                if (result.Succeeded)
                {
                    var receptionistUser = userManager.FindByNameAsync(receptionistEmail).Result;

                    // Claim - is a worker
                    var workerClaim = new Claim("IsWorker", "True");
                    var claimAdded = userManager.AddClaimAsync(receptionistUser, workerClaim).Result;

                    var receptionClaim = new Claim("Reception", "True");
                    claimAdded = userManager.AddClaimAsync(receptionistUser, receptionClaim).Result;

                }
            }

            // Seeding the Restaurant worker

            const string restaurantEmail = "restaurant@mail.dk";
            const string restaurantPassword = "password1";
            if (userManager == null)
                throw new ArgumentNullException(nameof(userManager));

            if (userManager.FindByNameAsync(restaurantEmail).Result == null)
            {
                var user = new IdentityUser();
                user.UserName = restaurantEmail;
                user.Email = restaurantEmail;
                user.EmailConfirmed = true;
                IdentityResult result = userManager.CreateAsync(user, restaurantPassword).Result;

                if (result.Succeeded)
                {
                    var restaurantUser = userManager.FindByNameAsync(restaurantEmail).Result;

                    // Claim - is a worker
                    var workerClaim = new Claim("IsWorker", "True");
                    var claimAdded = userManager.AddClaimAsync(restaurantUser, workerClaim).Result;

                    var restaurantClaim = new Claim("Restaurant", "True");
                    claimAdded = userManager.AddClaimAsync(restaurantUser, restaurantClaim).Result;

                }
            }

            // Seeding the Kitchen worker

            const string kitchenEmail = "kitchen@mail.dk";
            const string kitchenPassword = "password1";
            if (userManager == null)
                throw new ArgumentNullException(nameof(userManager));

            if (userManager.FindByNameAsync(kitchenEmail).Result == null)
            {
                var user = new IdentityUser();
                user.UserName = kitchenEmail;
                user.Email = kitchenEmail;
                user.EmailConfirmed = true;
                IdentityResult result = userManager.CreateAsync(user, kitchenPassword).Result;

                if (result.Succeeded)
                {
                    var kitchenUser = userManager.FindByNameAsync(kitchenEmail).Result;

                    // Claim - is a worker
                    var workerClaim = new Claim("IsWorker", "True");
                    var claimAdded = userManager.AddClaimAsync(kitchenUser, workerClaim).Result;

                    var kitchenClaim = new Claim("Kitchen", "True");
                    claimAdded = userManager.AddClaimAsync(kitchenUser, kitchenClaim).Result;

                }
            }
        }
    }
}
