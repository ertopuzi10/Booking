namespace Booking.Application.Features.Notifications
{
    public static class WelcomeEmail
    {
        public static string GetSubject() => "Welcome to Booking!";

        public static string GetBody(string userName) =>
            $"Hi {userName},\n\nWelcome to Booking! Your account has been created successfully.\n\nStart exploring properties and book your next stay.\n\nThe Booking Team";
    }
}
