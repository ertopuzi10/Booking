using System;
using System.Threading.Tasks;

namespace Booking.Application.Abstractions
{
    public interface IEmailService
    {
        Task SendWelcomeAsync(string toEmail, string userName);
        Task SendNewBookingRequestAsync(string toEmail, string hostName, string guestName, string propertyName, DateTime startDate, DateTime endDate);
        Task SendBookingConfirmedAsync(string toEmail, string guestName, string propertyName, DateTime startDate, DateTime endDate);
        Task SendBookingRejectedAsync(string toEmail, string guestName, string propertyName);
        Task SendBookingCancelledAsync(string guestEmail, string hostEmail, string guestName, string hostName, string propertyName);
    }
}
