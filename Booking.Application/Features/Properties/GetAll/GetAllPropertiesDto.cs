using System;

namespace Booking.Application.Features.Properties.GetAll
{
    public class GetAllPropertiesDto
    {
        public Guid Id { get; init; }
        public Guid OwnerId { get; init; }
        public string Name { get; init; } = null!;
        public string Description { get; init; } = null!;
        public string PropertyType { get; init; } = null!;
        public Guid AddressId { get; init; }
        public int MaxGuests { get; init; }
        public bool IsActive { get; init; }
        public bool IsApproved { get; init; }
        public decimal PricePerNight { get; init; }
        public double? AverageRating { get; init; }
    }
}
