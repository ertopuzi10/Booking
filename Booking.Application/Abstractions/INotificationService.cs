using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Booking.Application.Abstractions
{
    public interface INotificationService
    {
        Task SendToUserAsync(Guid userId, string type, string message);
        Task SendToUsersAsync(IEnumerable<Guid> userIds, string type, string message);
    }
}
