using Microsoft.EntityFrameworkCore;
using server.Models.Domain;

namespace server.Context
{
    public static class SampleData
    {
        public static void CreateSampleData(ModelBuilder modelBuilder)
        {
            // Sample users
            modelBuilder.Entity<User>().HasData(new User(1, "Code Master Flex", "code_master@example.com"));
            modelBuilder.Entity<User>().HasData(new User(2, "Debug Diva", "debug_diva@example.com"));
            // Sample rooms
            modelBuilder.Entity<Room>().HasData(new Room(1, "Binary Bunker"));
            modelBuilder.Entity<Room>().HasData(new Room(2, "Algorithm Alcove"));
            // Sample seats
            modelBuilder.Entity<Seat>().HasData(new Seat(1, 1));
            modelBuilder.Entity<Seat>().HasData(new Seat(2, 1));
            // Sample bookings
            modelBuilder.Entity<Booking>().HasData(new Booking(1, 1, 1, DateTime.Now));
            modelBuilder.Entity<Booking>().HasData(new Booking(2, 2, 2, DateTime.Now.AddDays(1)));
        }
    }
}