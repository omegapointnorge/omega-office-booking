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
           
            // End of User setup

            // Seat setup
            modelBuilder.Entity<Seat>()
                .HasKey(seat => seat.Id);

            modelBuilder.Entity<Seat>()
                .Property(seat => seat.Id)
                .ValueGeneratedOnAdd();
  
            // End of Seat setup

            // Room setup
            modelBuilder.Entity<Room>()
                .HasKey(room => room.Id);

            modelBuilder.Entity<Room>()
                .Property(room => room.Id)
                .ValueGeneratedOnAdd();
            
            // End of Room setup
            
            SeedData(modelBuilder);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            // Sample users
            var users = new List<User>
            {
                new User(1, "John Doe", "john@example.com", new List<Booking>()),
                new User(2, "Jane Doe", "jane@example.com", new List<Booking>())
            };
            modelBuilder.Entity<User>().HasData(users);

            // Sample rooms
            var rooms = new List<Room>
            {
                new Room(1, "Store Rommet", new List<Seat>()),
                new Room(2, "Lille Rommet", new List<Seat>())
            };
            modelBuilder.Entity<Room>().HasData(rooms);

            // Sample seats
            var seats = new List<Seat>
            {
                new Seat(1, 1, new List<Booking>()),
                new Seat(2, 1, new List<Booking>()),
                new Seat(3, 2, new List<Booking>())
            };
            modelBuilder.Entity<Seat>().HasData(seats);

            // Sample bookings
            var bookings = new List<Booking>
            {
                new Booking(1, 1, 1, DateTime.Now),
                new Booking(2, 2, 2, DateTime.Now.AddDays(1))
            };
            modelBuilder.Entity<Booking>().HasData(bookings);
        }
        
    }
}