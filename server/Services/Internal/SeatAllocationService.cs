using Microsoft.Graph;
using server.Models.DTOs.Internal;
using server.Repository;
using server.Services.External;

namespace server.Services.Internal;

public class SeatAllocationService : ISeatAllocationService
{
    private readonly ISeatAllocationRepository _seatAllocationRepository;
    private readonly GraphServiceClient _graphServiceClient;
    private readonly ITelemetryService _telemetryClient;

    public SeatAllocationService(ITelemetryService telemetryClient, ISeatAllocationRepository seatAllocationRepository, IBookingRepository bookingRepository, GraphServiceClient graphServiceClient)
    {
        _seatAllocationRepository = seatAllocationRepository;
        _graphServiceClient = graphServiceClient;
        _telemetryClient = telemetryClient;
    }


    public async Task<IEnumerable<SeatAllocationDetails>> GetAllSeatAssignmentDetails()
    {
        var seatAllocations = await _seatAllocationRepository.GetAsync();
        var seatAllocationDetails = new List<SeatAllocationDetails>();

        foreach (var seatAllocation in seatAllocations)
        {
            var users = await _graphServiceClient.Users.GetAsync(rc =>
            {
                rc.QueryParameters.Filter = $"mail eq '{seatAllocation.Email}'";
            });
            if (users == null || users.Value?.FirstOrDefault() == null) {
                _telemetryClient.TrackTrace($"cannot find the '{seatAllocation.Email}'");
                continue;
            }
            seatAllocationDetails.Add(new SeatAllocationDetails(users.Value.FirstOrDefault().Id, users.Value[0].DisplayName, users.Value[0].Mail, seatAllocation.SeatId));
        }
        return seatAllocationDetails;
    }
}