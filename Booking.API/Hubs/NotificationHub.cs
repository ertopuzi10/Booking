using System;
using System.Threading.Tasks;
using Booking.Application.Hubs;

namespace Booking.API.Hubs
{
    public class NotificationHub : NotificationHubBase
    {
        public override async Task OnConnectedAsync()
        {
            var userIdClaim = Context.User?.FindFirst("id")?.Value;
            if (!string.IsNullOrEmpty(userIdClaim))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userIdClaim);
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userIdClaim = Context.User?.FindFirst("id")?.Value;
            if (!string.IsNullOrEmpty(userIdClaim))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, userIdClaim);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
