
namespace server.Services.Internal;

public interface ISeatService
{
    public Task<IEnumerable<int>> GetUnavailableSeatIds();
}
