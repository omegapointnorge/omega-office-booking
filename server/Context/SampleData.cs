using Microsoft.EntityFrameworkCore;
using server.Models.Domain;

namespace server.Context
{
    public static class SampleData
    {
        public static void CreateSampleData(ModelBuilder modelBuilder)
        {
            // Sample rooms
            modelBuilder.Entity<Room>().HasData(new Room(1, "Store Rommet"));
            modelBuilder.Entity<Room>().HasData(new Room(2, "Lille Rommet"));
            modelBuilder.Entity<Room>().HasData(new Room(3, "Salg"));
            modelBuilder.Entity<Room>().HasData(new Room(4, "Marie"));
            modelBuilder.Entity<Room>().HasData(new Room(5, "Okonomi"));
            modelBuilder.Entity<Room>().HasData(new Room(6, "Oystein"));
            // Sample seats
            // Generating Seats for room 1
            for (int i = 1; i <= 10; i++)
            {
                modelBuilder.Entity<Seat>().HasData(new Seat(i, 1, true));
            }
            // Generatings Seats for room 2
            for (int i = 11; i <= 15; i++)
            {
                modelBuilder.Entity<Seat>().HasData(new Seat(i, 2, true));
            }
            // Generatings Seats for room 3
            for (int i = 16; i <= 22; i++)
            {
                //manual setting of unavailable for now, it can be remove in later step
                bool isAvailable = i == 17;
                modelBuilder.Entity<Seat>().HasData(new Seat(i, 3, isAvailable));
            }
            // Generatings Seats for room 4
            for (int i = 23; i <= 25; i++)
            {
                modelBuilder.Entity<Seat>().HasData(new Seat(i, 4, true));
            }

            modelBuilder.Entity<Seat>().HasData(new Seat(26, 5, true));
            modelBuilder.Entity<Seat>().HasData(new Seat(27, 6, true));

            modelBuilder.Entity<SeatAllocation>().HasData(new SeatAllocation(1, 2, "eric.bondoux@omegapoint.no"));

        }
    }
}