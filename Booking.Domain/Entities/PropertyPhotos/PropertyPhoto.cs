namespace Booking.Domain.Entities
{
    public class PropertyPhoto
    {
        public Guid Id { get; set; }

        public Guid PropertyId { get; set; }
        public Properties Property { get; set; } = null!;

        public string Url { get; set; } = null!;
        public int DisplayOrder { get; set; }
    }
}
