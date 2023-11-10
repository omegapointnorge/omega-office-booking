using Microsoft.EntityFrameworkCore;

namespace server.Data;
    
    public static class SeedData
{
    public static void Seed(ModelBuilder builder)
    {
        builder.Entity<SeatEntity>().HasData(new List<SeatEntity> {
            new SeatEntity {
                Id = 1,
                Room = "12",
                Price = 900000
            },
            new SeatEntity
            {
                Id = 2,
                Room = "89",
                Price = 500000
            },
            new SeatEntity
            {
                Id = 3,
                Room = "23",
                Price = 200500
            }
          
        });
        builder.Entity<BookingEntity>().HasData(new List<BookingEntity>
        {
            new BookingEntity { Id = 1, SeatId = 1, Bookingder = "Sonia Reading" },
            new BookingEntity { Id = 2, SeatId = 1, Bookingder = "Dick Johnson" },
        });
    }
}