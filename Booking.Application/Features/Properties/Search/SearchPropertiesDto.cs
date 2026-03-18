using System;

namespace Booking.Application.Features.Properties.Search
{
    public class SearchPropertiesDto
    {
        public Guid Id { get; init; }
        public Guid OwnerId { get; init; }
        public string Name { get; init; } = null!;
        public string Description { get; init; } = null!;
        public string PropertyType { get; init; } = null!;
        public int MaxGuests { get; init; }
        public bool IsActive { get; init; }
        public bool IsApproved { get; init; }
        public decimal PricePerNight { get; init; }
        public double? AverageRating { get; init; }
        public string? AddressCity { get; init; }
        public string? AddressCountry { get; init; }
    }
}
