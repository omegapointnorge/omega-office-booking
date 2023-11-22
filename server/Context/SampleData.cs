using Microsoft.EntityFrameworkCore;
using server.Models.Domain;

namespace server.Context
{
    public static class SampleData
    {
        public static void CreateSampleData(ModelBuilder modelBuilder)
        {
            // Sample users
            var users = new List<User>
            {
                new User(1, "Code Master Flex", "code_master@example.com"),
                new User(2, "Debug Diva", "debug_diva@example.com"),
                new User(3, "Syntax Sorcerer", "syntax_sorcerer@example.com"),
                new User(4, "Pixel Picasso", "pixel_picasso@example.com"),
                new User(5, "Console Cowboy", "console_cowboy@example.com"),
                new User(6, "Bit Boffin", "bit_boffin@example.com")
            };
            modelBuilder.Entity<User>().HasData(users);

            // Sample rooms
            var rooms = new List<Room>
            {
                new Room(1, "Binary Bunker"),
                new Room(2, "Algorithm Alcove"),
                new Room(3, "Cache Corner"),
                new Room(4, "Syntax Sanctuary"),
                new Room(5, "Exception Escape")
            };
            modelBuilder.Entity<Room>().HasData(rooms);

            // Sample seats
            var seats = new List<Seat>
            {
                new Seat(1, 1),
                new Seat(2, 1),
                new Seat(3, 2),
                new Seat(4, 3),
                new Seat(5, 3),
                new Seat(6, 4),
                new Seat(7, 4),
                new Seat(8, 5),
                new Seat(9, 5)
            };
            modelBuilder.Entity<Seat>().HasData(seats);

            // Sample bookings
            var bookings = new List<Booking>
            {
                new Booking(1, 1, 1, DateTime.Now),
                new Booking(2, 2, 2, DateTime.Now.AddDays(1)),
                new Booking(3, 3, 3, DateTime.Now.AddHours(2)),
                new Booking(4, 4, 4, DateTime.Now.AddDays(2)),
                new Booking(5, 5, 5, DateTime.Now.AddHours(3)),
                new Booking(6, 6, 6, DateTime.Now.AddDays(3))
            };
            modelBuilder.Entity<Booking>().HasData(bookings);
        }
    }
}