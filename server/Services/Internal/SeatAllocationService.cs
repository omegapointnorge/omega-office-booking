using Microsoft.Graph;
using server.Models.DTOs.Internal;
using server.Repository;

namespace server.Services.Internal;

public class SeatAllocationService : ISeatAllocationService
{
    private readonly ISeatAllocationRepository _seatAllocationRepository;
    private readonly GraphServiceClient _graphServiceClient;

    public SeatAllocationService(ISeatAllocationRepository seatAllocationRepository, IBookingRepository bookingRepository, GraphServiceClient graphServiceClient)
    {
        _seatAllocationRepository = seatAllocationRepository;
        _graphServiceClient = graphServiceClient;
    }


    public async Task<IEnumerable<SeatAllocationDetails>> GetAllSeatAssignmentDetails()
    {
        var seatAllocations = await _seatAllocationRepository.GetAsync();
        var seatAllocationDetails = new List<SeatAllocationDetails>();

        var UserClaimsList = new List<SeatAllocationDetails>();

        foreach (var seatAllocation in seatAllocations)
        {
            var users = await _graphServiceClient.Users.GetAsync(rc =>
            {
                rc.QueryParameters.Filter = $"mail eq '{seatAllocation.Email}'";
            });

            seatAllocationDetails.Add(new SeatAllocationDetails(users.Value[0].Id, users.Value[0].DisplayName, users.Value[0].Mail, seatAllocation.SeatId));
        }
        return seatAllocationDetails;
    }
}