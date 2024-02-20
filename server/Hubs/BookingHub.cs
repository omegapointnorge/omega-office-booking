using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.SignalR;

//TODO: namespace
namespace SignalRChat.Hubs
{
    public class BookingHub : Hub
    {
        public async Task NotifyBookingUpdate(string user, string seatId)
        {
            await Clients.All.SendAsync("BookingNotification", user, seatId);
        }
    }
}
