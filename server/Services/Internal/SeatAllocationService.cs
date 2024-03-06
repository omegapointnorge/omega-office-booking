using Microsoft.Graph;
using server.Models.Domain;
using server.Repository;

namespace server.Services.Internal;

public class SeatAllocationService : ISeatAllocationService
{
    private readonly ISeatAllocationRepository _seatAllocationRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly GraphServiceClient _graphServiceClient;

    public SeatAllocationService(ISeatAllocationRepository seatAllocationRepository, IBookingRepository bookingRepository, GraphServiceClient graphServiceClient)
    {
        _seatAllocationRepository = seatAllocationRepository;
        _bookingRepository = bookingRepository;
        _graphServiceClient = graphServiceClient;
    }


    public async Task<IEnumerable<SeatAllocation>> GetAllSeatAssignments()
    {
        return await _seatAllocationRepository.GetAsync();
    }

    public async Task GenerateSeatAssignmentBookings(IEnumerable<SeatAllocation> seatAssignments, DateTime todayPlusOneMonth)
    {
        foreach (var seatAssignment in seatAssignments)
        {
            var booking = new Booking(seatAssignment.UserId, seatAssignment.DisplayName, seatAssignment.SeatId, todayPlusOneMonth, todayPlusOneMonth.Date);
            await _bookingRepository.AddAsync(booking);
        }

        await _bookingRepository.SaveAsync();
    }

    public async Task DeleteBookings(IEnumerable<SeatAllocation> seatAssignments, DateTime yesterday)
    {
        foreach (var seatAssignment in seatAssignments)
        {
            var booking = await _bookingRepository.GetBookingDetailsBySeatIdAndDateAndUserId(seatAssignment.UserId, seatAssignment.SeatId, yesterday);
            if (booking != null)
            {
                await _bookingRepository.Delete(booking);
            }
        }
        await _bookingRepository.SaveAsync();
    }


}
