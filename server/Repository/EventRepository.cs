using server.Context;
using server.Models.Domain;

namespace server.Repository
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        private readonly OfficeDbContext _dbContext;
        public EventRepository(OfficeDbContext context) : base(context)
        {
            _dbContext = context;
        }
    }
}