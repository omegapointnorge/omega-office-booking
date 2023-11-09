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
                Country = "Switzerland",
                Description = "A superb detached Victorian property on one of the town's finest roads, within easy reach of Barnes Village. The property has in excess of 6000 sq/ft of accommodation, a driveway and landscaped garden.",
                Price = 900000
            },
            new SeatEntity
            {
                Id = 2,
                Room = "89",
                Country = "Switzerland",
                Description = "This impressive family home, which dates back to approximately 1840, offers original period features throughout and is set back from the road with off street parking for up to six cars and an original Coach Seat, which has been incorporated into the main Seat to provide further accommodation. ",
                Price = 500000
            },
            new SeatEntity
            {
                Id = 3,
                Room = "23",
                Country = "The Netherlands",
                Description = "This Seat has been designed and built to an impeccable standard offering luxurious and elegant living. The accommodation is arranged over four floors comprising a large entrance hall, living room with tall sash windows, dining room, study and WC on the ground floor.",
                Price = 200500
            },
            new SeatEntity
            {
                Id = 4,
                Room = "12",
                Country = "The Netherlands",
                Description = "Discreetly situated a unique two storey period home enviably located on the corner of Krom Road and Recht Road offering seclusion and privacy. The Seat features a magnificent double height reception room with doors leading directly out onto a charming courtyard garden.",
                Price = 259500
            },
            new SeatEntity
            {
                Id = 5,
                Room = "12",
                Country = "The Netherlands",
                Description = "This luxurious three bedroom flat is contemporary in style and benefits from the use of a gymnasium and a reserved underground parking space.",
                Price = 400500
            }
        });
        builder.Entity<BookingEntity>().HasData(new List<BookingEntity>
        {
            new BookingEntity { Id = 1, SeatId = 1, Bookingder = "Sonia Reading", Amount = 200000 },
            new BookingEntity { Id = 2, SeatId = 1, Bookingder = "Dick Johnson", Amount = 202400 },
            new BookingEntity { Id = 3, SeatId = 2, Bookingder = "Mohammed Vahls", Amount = 302400 },
            new BookingEntity { Id = 4, SeatId = 2, Bookingder = "Jane Williams", Amount = 310500 },
            new BookingEntity { Id = 5, SeatId = 2, Bookingder = "John Kepler", Amount = 315400 },
            new BookingEntity { Id = 6, SeatId = 3, Bookingder = "Bill Mentor", Amount = 201000 },
            new BookingEntity { Id = 7, SeatId = 4, Bookingder = "Melissa Kirk", Amount = 410000 },
            new BookingEntity { Id = 8, SeatId = 4, Bookingder = "Scott Max", Amount = 450000 },
            new BookingEntity { Id = 9, SeatId = 4, Bookingder = "Christine James", Amount = 470000 },
            new BookingEntity { Id = 10, SeatId = 5, Bookingder = "Omesh Carim", Amount = 450000 },
            new BookingEntity { Id = 11, SeatId = 5, Bookingder = "Charlotte Max", Amount = 150000 },
            new BookingEntity { Id = 12, SeatId = 5, Bookingder = "Marcus Scott", Amount = 170000 },
        });
    }
}