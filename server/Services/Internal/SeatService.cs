using Microsoft.Graph;
using server.Models.Domain;
using server.Models.DTOs;
using server.Models.DTOs.Internal;
using server.Repository;

namespace server.Services.Internal
{
    public class SeatService : ISeatService
    {
        private readonly ISeatRepository _seatRepository;
        private readonly GraphServiceClient _graphServiceClient;

        public SeatService(ISeatRepository seatRepository, GraphServiceClient graphServiceClient)
        {
            _graphServiceClient = graphServiceClient;
            _seatRepository = seatRepository;

        }
        public async Task<IEnumerable<SeatDto>> GetAllSeats()
        {
            IEnumerable<Seat> allSeats = await _seatRepository.GetAsync();
            return allSeats.Select(seat => new SeatDto(seat.Id, seat.RoomId));
        }

        public async Task<IEnumerable<SeatAllocationDetails>> GetAllSeatAssignmentDetails()
        {
            var seatsWithOwner = await _seatRepository.GetAllAsync(seat => seat.SeatOwnerEmail != null);

            var seatAllocationDetails = new List<SeatAllocationDetails>();

            foreach (var seat in seatsWithOwner)
            {
                var users = await _graphServiceClient.Users.GetAsync(rc =>
                {
                    rc.QueryParameters.Filter = $"mail eq '{seat.SeatOwnerEmail}'";
                });

                if (users.Value.Count > 0)
                {
                    seatAllocationDetails.Add(new SeatAllocationDetails(users.Value[0].Id, users.Value[0].DisplayName, users.Value[0].Mail, seat.Id));
                }

            }
            return seatAllocationDetails;
        }
    }
}