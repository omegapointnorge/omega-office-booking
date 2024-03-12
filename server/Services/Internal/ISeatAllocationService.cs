using server.Models.DTOs.Internala;

namespace server.Services.Internal;

public interface ISeatAllocationService
{
    Task<IEnumerable<SeatAllocationDetails>> GetAllSeatAssignmentDetails();
}