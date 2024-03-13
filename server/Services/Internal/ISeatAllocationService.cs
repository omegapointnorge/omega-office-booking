using server.Models.DTOs.Internal;

namespace server.Services.Internal;

public interface ISeatAllocationService
{
    Task<IEnumerable<SeatAllocationDetails>> GetAllSeatAssignmentDetails();
}