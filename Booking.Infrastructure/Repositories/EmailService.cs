using System;
using System.Threading.Tasks;
using Booking.Application.Abstractions;
using Booking.Application.Features.Notifications;
using Booking.Infrastructure.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Booking.Infrastructure.Repositories
{
    public class EmailService : IEmailService
    {
        private readonly SendGridOptions _options;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<SendGridOptions> options, ILogger<EmailService> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        public async Task SendWelcomeAsync(string toEmail, string userName)
        {
            await SendAsync(
                toEmail,
                WelcomeEmail.GetSubject(),
                WelcomeEmail.GetBody(userName));
        }

        public async Task SendNewBookingRequestAsync(string toEmail, string hostName, string guestName, string propertyName, DateTime startDate, DateTime endDate)
        {
            var subject = $"New booking request for {propertyName}";
            var body = $"Hi {hostName},\n\nYou have a new booking request for {propertyName}.\n\nGuest: {guestName}\nCheck-in: {startDate:MMMM d, yyyy}\nCheck-out: {endDate:MMMM d, yyyy}\n\nPlease log in to confirm or reject the booking.\n\nThe Booking Team";
            await SendAsync(toEmail, subject, body);
        }

        public async Task SendBookingConfirmedAsync(string toEmail, string guestName, string propertyName, DateTime startDate, DateTime endDate)
        {
            await SendAsync(
                toEmail,
                BookingConfirmedEmail.GetSubject(),
                BookingConfirmedEmail.GetBody(guestName, propertyName, startDate, endDate));
        }

        public async Task SendBookingRejectedAsync(string toEmail, string guestName, string propertyName)
        {
            await SendAsync(
                toEmail,
                BookingRejectedEmail.GetSubject(),
                BookingRejectedEmail.GetBody(guestName, propertyName));
        }

        public async Task SendBookingCancelledAsync(string guestEmail, string hostEmail, string guestName, string hostName, string propertyName)
        {
            await SendAsync(
                guestEmail,
                BookingCancelledEmail.GetSubject(),
                BookingCancelledEmail.GetGuestBody(guestName, propertyName));

            await SendAsync(
                hostEmail,
                BookingCancelledEmail.GetSubject(),
                BookingCancelledEmail.GetHostBody(hostName, guestName, propertyName));
        }

        private async Task SendAsync(string toEmail, string subject, string body)
        {
            try
            {
                var client = new SendGridClient(_options.ApiKey);
                var from = new EmailAddress(_options.FromEmail, _options.FromName);
                var to = new EmailAddress(toEmail);
                var msg = MailHelper.CreateSingleEmail(from, to, subject, body, null);
                await client.SendEmailAsync(msg);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {ToEmail} with subject '{Subject}'", toEmail, subject);
            }
        }
    }
}
