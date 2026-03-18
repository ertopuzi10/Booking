using System;

namespace Booking.Application.Features.Reviews.Queries.GetReviewsByProperty
{
    public class GetReviewsByPropertyDto
    {
        public Guid Id { get; init; }
        public Guid BookingId { get; init; }
        public string GuestFullName { get; init; } = string.Empty;
        public int Rating { get; init; }
        public string? Comment { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}
