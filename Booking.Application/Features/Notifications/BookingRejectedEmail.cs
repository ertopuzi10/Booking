namespace Booking.Application.Features.Notifications
{
    public static class BookingRejectedEmail
    {
        public static string GetSubject() => "Your booking request was not accepted";

        public static string GetBody(string guestName, string propertyName) =>
            $"Hi {guestName},\n\nUnfortunately, your booking request for {propertyName} has been declined by the host.\n\nPlease browse our other available properties and try again.\n\nThe Booking Team";
    }
}
