using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models.Domain;

namespace server.Context;

    public class OfficeDbContextLokal : DbContext
{
    public OfficeDbContextLokal(DbContextOptions<OfficeDbContextLokal> options) : base(options) { }
    public DbSet<Seat> Seats => Set<Seat>();
    public DbSet<Booking> Bookings => Set<Booking>();

    public DbSet<User> Users => Set<User>();
    public DbSet<Room> Rooms => Set<Room>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
            SeedData.Seed(builder);
    }
}