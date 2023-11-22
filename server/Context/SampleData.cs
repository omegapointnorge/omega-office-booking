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
                new User( "Code Master Flex", "code_master@example.com"),
                new User( "Debug Diva", "debug_diva@example.com"),
                new User( "Syntax Sorcerer", "syntax_sorcerer@example.com"),
                new User( "Pixel Picasso", "pixel_picasso@example.com"),
                new User( "Console Cowboy", "console_cowboy@example.com"),
                new User( "Bit Boffin", "bit_boffin@example.com")
            };
            modelBuilder.Entity<User>().HasData(users);

            // Sample rooms
            var rooms = new List<Room>
            {
                new Room( "Binary Bunker"),
                new Room( "Algorithm Alcove"),
                new Room( "Cache Corner"),
                new Room( "Syntax Sanctuary"),
                new Room( "Exception Escape")
            };
            modelBuilder.Entity<Room>().HasData(rooms);

            // Sample seats
            var seats = new List<Seat>
            {
                new Seat( 1),
                new Seat( 1),
                new Seat( 2),
                new Seat( 3),
                new Seat( 3),
                new Seat( 4),
                new Seat( 4),
                new Seat( 5),
                new Seat( 5)
            };
            modelBuilder.Entity<Seat>().HasData(seats);

            // Sample bookings
            var bookings = new List<Booking>
            {
                new Booking( 1, 1, DateTime.Now),
                new Booking( 2, 2, DateTime.Now.AddDays(1)),
                new Booking( 3, 3, DateTime.Now.AddHours(2)),
                new Booking( 4, 4, DateTime.Now.AddDays(2)),
                new Booking( 5, 5, DateTime.Now.AddHours(3)),
                new Booking( 6, 6, DateTime.Now.AddDays(3))
            };
            modelBuilder.Entity<Booking>().HasData(bookings);
        }
    }
}