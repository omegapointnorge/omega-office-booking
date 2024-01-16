using server.Context;
using server.Models.Domain;

namespace server.Repository
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private readonly OfficeDbContext _dbContext;

        public BookingRepository(OfficeDbContext context) : base(context)
        {
            _dbContext = context;
        }
    }
}