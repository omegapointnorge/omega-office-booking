using server.Context;
using server.Models.Domain;

namespace server.Repository;

public class SeatRepository : Repository<Seat>, ISeatRepository
{
    public SeatRepository(OfficeDbContext context) : base(context)
    {
    }
}
