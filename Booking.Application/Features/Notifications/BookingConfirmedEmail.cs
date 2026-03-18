using System;

namespace Booking.Application.Features.Notifications
{
    public static class BookingConfirmedEmail
    {
        public static string GetSubject() => "Your booking has been confirmed!";

        public static string GetBody(string guestName, string propertyName, DateTime startDate, DateTime endDate) =>
            $"Hi {guestName},\n\nGreat news! Your booking for {propertyName} has been confirmed.\n\nCheck-in: {startDate:MMMM d, yyyy}\nCheck-out: {endDate:MMMM d, yyyy}\n\nWe hope you enjoy your stay!\n\nThe Booking Team";
    }
}
