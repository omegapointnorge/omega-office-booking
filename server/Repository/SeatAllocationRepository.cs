using server.Context;
using server.Models.Domain;

namespace server.Repository;

public class SeatAllocationRepository(OfficeDbContext context) : Repository<SeatAllocation>(context), ISeatAllocationRepository
{
}