using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Booking.Application.Abstractions;
using Booking.Application.Hubs;
using Microsoft.AspNetCore.SignalR;

using Microsoft.Extensions.Logging;

namespace Booking.Infrastructure.Repositories
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHubBase> _hubContext;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(IHubContext<NotificationHubBase> hubContext, ILogger<NotificationService> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        public async Task SendToUserAsync(Guid userId, string type, string message)
        {
            try
            {
                await _hubContext.Clients.Group(userId.ToString())
                    .SendAsync("ReceiveNotification", new { type, message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send SignalR notification to user {UserId}", userId);
            }
        }

        public async Task SendToUsersAsync(IEnumerable<Guid> userIds, string type, string message)
        {
            foreach (var userId in userIds)
            {
                await SendToUserAsync(userId, type, message);
            }
        }
    }
}
