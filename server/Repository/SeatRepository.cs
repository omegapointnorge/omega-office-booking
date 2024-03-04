using server.Context;
using server.Models.Domain;

namespace server.Repository;

public class SeatRepository(OfficeDbContext context) : Repository<Seat>(context), ISeatRepository
{
}
