using server.Context;
using server.Models.Domain;

namespace server.Repository;

public class JobExecutionRepository(OfficeDbContext context) : Repository<JobExecutionLog>(context), IRepository<JobExecutionLog>
{
}