using server.Models.Domain;

namespace server.Services.Internal;

public interface ISeatAllocationService
{
    Task DeleteBookings(IEnumerable<SeatAllocation> seatAssignments, DateTime yesterday);
    Task GenerateSeatAssignmentBookings(IEnumerable<SeatAllocation> seatAssignmentTasks, DateTime todayPlusOneMonth);
    Task<IEnumerable<SeatAllocation>> GetAllSeatAssignments();
}
