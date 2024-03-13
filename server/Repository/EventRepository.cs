using server.Context;
using server.Models.Domain;

namespace server.Repository
{
    public class EventRepository(OfficeDbContext context) : Repository<Event>(context), IEventRepository
    {
    }
}