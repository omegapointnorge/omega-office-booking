using server.Context;
using server.Models.DTOs;

namespace server.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly OfficeDbContext dbContext;

        public BookingRepository(OfficeDbContext officeDbContext)
        {
            dbContext = officeDbContext;
        }
        public Task<List<BookingDto>> GetAllBookings()
        {
            throw new NotImplementedException();
        }
    }
}