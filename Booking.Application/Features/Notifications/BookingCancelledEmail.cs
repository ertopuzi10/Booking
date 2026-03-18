namespace Booking.Application.Features.Notifications
{
    public static class BookingCancelledEmail
    {
        public static string GetSubject() => "A booking has been cancelled";

        public static string GetGuestBody(string guestName, string propertyName) =>
            $"Hi {guestName},\n\nYour booking for {propertyName} has been cancelled.\n\nIf you did not request this cancellation, please contact our support team.\n\nThe Booking Team";

        public static string GetHostBody(string hostName, string guestName, string propertyName) =>
            $"Hi {hostName},\n\nThe booking by {guestName} for {propertyName} has been cancelled.\n\nThe dates are now available again for new bookings.\n\nThe Booking Team";
    }
}
