using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models.Domain;
namespace server.Context;

    public class OfficeDbContext : DbContext
{
    public OfficeDbContext(DbContextOptions<OfficeDbContext> options) : base(options) { }
    public DbSet<SeatEntity> Seats => Set<SeatEntity>();
    public DbSet<BookingEntity> Bookings => Set<BookingEntity>();


    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        options.UseSqlite($"Data Source={Path.Join(path, "Seats.db")}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        SeedData.Seed(modelBuilder);
    }
}