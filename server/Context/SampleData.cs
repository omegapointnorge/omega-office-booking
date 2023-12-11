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
            modelBuilder.Entity<User>().HasData(new User(3, "Omcma Diva", "debug_omacgi@example.com"));
            // Sample rooms
            modelBuilder.Entity<Room>().HasData(new Room(1, "Store Rommet"));
            modelBuilder.Entity<Room>().HasData(new Room(2, "Lille Rommet"));
            // Sample seats
            // Generating Seats for room 1
            for (int i = 1; i <= 10; i++) 
            {
                modelBuilder.Entity<Seat>().HasData(new Seat(i, 1));
            }
            // Generatings Seats for room 2
            for (int i = 11; i <= 15; i++) 
            {
                modelBuilder.Entity<Seat>().HasData(new Seat(i, 2));
            }
            // Sample bookings
            modelBuilder.Entity<Booking>().HasData(new Booking(1, 1, 1, new DateTime(2023, 12, 7, 14, 44, 11, 768, DateTimeKind.Local).AddTicks(9580)));
            modelBuilder.Entity<Booking>().HasData(new Booking(2, 2, 2, new DateTime(2023, 12, 5, 14, 44, 11, 768, DateTimeKind.Local).AddTicks(9580)));
        }
    }
}