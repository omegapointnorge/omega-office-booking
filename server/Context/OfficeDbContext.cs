using Microsoft.EntityFrameworkCore;
using server.Models.Domain;
namespace server.Context
{
    public class OfficeDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public OfficeDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}