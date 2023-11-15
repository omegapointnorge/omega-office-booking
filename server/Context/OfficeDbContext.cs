using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models.Domain;

namespace server.Context;

    public class OfficeDbContext : DbContext
{
    public OfficeDbContext(DbContextOptions<OfficeDbContext> options) : base(options) { }
    public DbSet<Seat> Seats => Set<Seat>();
    public DbSet<Booking> Bookings => Set<Booking>();

    public DbSet<User> Users => Set<User>();


    protected override void OnModelCreating(ModelBuilder builder)
    {
            SeedData.Seed(builder);
    }
}