namespace Booking.Domain.Entities
{
    public class PropertyAmenity
    {
        public Guid Id { get; set; }

        public Guid PropertyId { get; set; }
        public Properties Property { get; set; } = null!;

        public string Name { get; set; } = null!;
    }
}
