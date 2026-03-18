namespace Booking.Application.Features.Properties.Create
{
    public class CreatePropertyDto
    {
        public Guid OwnerId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PropertyType { get; set; } = null!;
        public Guid AddressId { get; set; }
        public int MaxGuests { get; set; }
    }
}