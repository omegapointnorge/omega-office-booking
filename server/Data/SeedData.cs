using Microsoft.EntityFrameworkCore;
using server.Models.Domain;

namespace server.Data;
    
    public static class SeedData
{
    public static void Seed(ModelBuilder builder)
    {

        builder.Entity<Room>().HasData(new List<Room> {
            new Room {
                Id = 1,

                Name = "stor rom"
            },
            new Room
            {
                Id = 2,

                Name ="liten rom"
            },
             new Room {
                Id = 3,

                Name = "Mellonstor rom"
            }

        });
        builder.Entity<Seat>().HasData(new List<Seat> {
            new Seat {
                Id = 1,
           
                RoomId = 1
            },
            new Seat
            {
                Id = 2,
         
                RoomId = 2
            },
            new Seat
            {
                Id = 3,
           
                RoomId = 1
            },
              new Seat
            {
                Id = 4,

                RoomId = 3
            },

        });
        builder.Entity<Booking>().HasData(new List<Booking>
        {
            new Booking { Id = 1, SeatId = 1, UserId = 1 },
            new Booking { Id = 2, SeatId = 3, UserId = 2 },
        });

        builder.Entity<User>().HasData(new List<User>
        { 
            new User { Id = 1, Email = "abcReading@gmail.com", Name = "Soniauser Reading",PhoneNumber= "12344321" },
            new User { Id = 2, Email = "Reading@gmail.com", Name = "Dick Johnson",PhoneNumber= "900944321" },
            new User { Id = 3, Email = "Johnson@gmail.com", Name = "Johnson Johnson",PhoneNumber= "900456321" },
            new User { Id = 4, Email = "vicky.huangyuanxin@omegapoint.no", Name = "vicky Yuanxin Huang",PhoneNumber= "900456321" },
        });
    }
}