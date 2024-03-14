using Microsoft.Graph;
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
            var allSeats = await _seatRepository.GetAsync();
            var seatAllocationDetails = await GetAllSeatAssignmentDetails();

            var seatDtos = allSeats.Select(seat =>
            {
                var seatAssignmentDetail = seatAllocationDetails.FirstOrDefault(x => x.SeatId == seat.Id);
                return new SeatDto(seat.Id, seat.RoomId, seatAssignmentDetail?.User.Objectidentifier);
            }).ToList();

            return seatDtos;
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