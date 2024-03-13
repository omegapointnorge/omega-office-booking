using Microsoft.EntityFrameworkCore;
using server.Models.Domain;

namespace server.Context
{
    public class OfficeDbContext : DbContext
    {
        public DbSet<Booking> Booking { get; set; }


        public OfficeDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Booking setup
            modelBuilder.Entity<Booking>()
                .HasKey(booking => booking.Id);

            modelBuilder.Entity<Booking>()
                .Property(booking => booking.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Booking>()
                .HasOne(booking => booking.Seat)
                .WithMany(seat => seat.Bookings)
                .HasForeignKey(booking => booking.SeatId)
                .IsRequired();

            modelBuilder.Entity<Booking>()
                .HasOne(booking => booking.Event)
                .WithMany(e => e.Bookings)
                .HasForeignKey(booking => booking.EventId);

            modelBuilder.Entity<Booking>()
                .Property(booking => booking.BookingDateTime)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            modelBuilder.Entity<Booking>()
                .Property(booking => booking.BookingDateTime_DayOnly);

            modelBuilder.Entity<Booking>()
                .HasAlternateKey(booking => new { booking.SeatId, booking.BookingDateTime_DayOnly })
                .HasName("unique_seat_time_constraint");

            modelBuilder.Entity<Booking>()
                .Property<DateTime>("CreatedAt")
                .HasDefaultValueSql("GETDATE()");
            // End of Booking setup


            // Seat setup
            modelBuilder.Entity<Seat>()
                .HasKey(seat => seat.Id);

            modelBuilder.Entity<Seat>()
                .Property(seat => seat.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Seat>()
                .HasOne<Room>()
                .WithMany()
                .HasForeignKey(seat => seat.RoomId)
                .IsRequired();

            modelBuilder.Entity<Seat>()
                .HasMany(seat => seat.Bookings)
                .WithOne(booking => booking.Seat)
                .HasPrincipalKey(seat => seat.Id);

            // End of Seat setup

            // Room setup
            modelBuilder.Entity<Room>()
                .HasKey(room => room.Id);

            modelBuilder.Entity<Room>()
                .Property(room => room.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Room>()
                .Property(seat => seat.Name)
                .IsRequired();

            modelBuilder.Entity<Room>()
                .HasMany(room => room.Seats)
                .WithOne()
                .HasForeignKey(seat => seat.RoomId);

            // End of Room setup
            // Event setup
            modelBuilder.Entity<Event>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Event>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Event>()
                 .Property(e => e.Name);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Bookings)
                .WithOne(booking => booking.Event)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.Cascade);

            // End of Event setup

            // SeatAllocation setup
            modelBuilder.Entity<SeatAllocation>()
                .HasKey(seatAllocation => seatAllocation.Id);

            modelBuilder.Entity<SeatAllocation>()
                .Property(seatAllocation => seatAllocation.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<SeatAllocation>()
                .Property(seatAllocation => seatAllocation.UserId)
                .IsRequired();

            modelBuilder.Entity<SeatAllocation>()
                .Property(seatAllocation => seatAllocation.SeatId)
                .IsRequired();
            modelBuilder.Entity<SeatAllocation>()
                .HasAlternateKey(seatAllocation => new { seatAllocation.SeatId })
                .HasName("unique_seat_constraint");

            // End of SeatAllocation setup

            SampleData.CreateSampleData(modelBuilder);
        }

    }
}