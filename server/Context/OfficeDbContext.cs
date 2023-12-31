using Microsoft.EntityFrameworkCore;
using server.Models.Domain;

namespace server.Context
{
    public class OfficeDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }

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
                .HasOne(booking => booking.User)
                .WithMany(user => user.Bookings)
                .HasForeignKey(booking => booking.UserId)
                .IsRequired();

            modelBuilder.Entity<Booking>()
                .HasOne(booking => booking.Seat)
                .WithMany(seat => seat.Bookings)
                .HasForeignKey(booking => booking.SeatId)
                .IsRequired();

            modelBuilder.Entity<Booking>()
                .Property(booking => booking.BookingDateTime)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            // End of Booking setup

            // User setup
            modelBuilder.Entity<User>()
                .HasKey(user => user.Id);

            modelBuilder.Entity<User>()
                .Property(user => user.Id)
                .ValueGeneratedOnAdd();
            
            modelBuilder.Entity<User>()
                .Property(user => user.Name)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasIndex(user => user.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasMany(user => user.Bookings)
                .WithOne(booking => booking.User)
                .HasPrincipalKey(user => user.Id);

            // End of User setup

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

            SampleData.CreateSampleData(modelBuilder);
        }
        
    }
}