using server.Models.Domain;
using server.Repository;

namespace server.Services.Internal;

public class SeatAllocationService : ISeatAllocationService
{
    private readonly ISeatAllocationRepository _seatAllocationRepository;
    private readonly IBookingRepository _bookingRepository;

    public SeatAllocationService(ISeatAllocationRepository seatAllocationRepository, IBookingRepository bookingRepository)
    {
        _seatAllocationRepository = seatAllocationRepository;
        _bookingRepository = bookingRepository;
    }


    public async Task<IEnumerable<SeatAllocation>> GetAllSeatAssignments()
    {
        return await _seatAllocationRepository.GetAsync();
    }

    public async Task GenerateSeatAssignmentBookings(IEnumerable<SeatAllocation> seatAssignments, DateTime todayPlusOneMonth)
    {
        foreach (var seatAssignment in seatAssignments)
        {
            var existingBooking = await _bookingRepository.GetBookingDetailsBySeatIdAndDate(seatAssignment.SeatId, todayPlusOneMonth.Date);

            if (existingBooking != null)
            {
                continue;
            }

            var booking = new Booking(seatAssignment.UserId, seatAssignment.DisplayName, seatAssignment.SeatId, todayPlusOneMonth, todayPlusOneMonth.Date);
            await _bookingRepository.AddAsync(booking);
        }

        await _bookingRepository.SaveAsync();
    }


}
