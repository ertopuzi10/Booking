using System;

namespace Booking.Domain.Entities
{
    public class PropertySeasonalPrice
    {
        public Guid Id { get; set; }

        public Guid PropertyId { get; set; }
        public Properties Property { get; set; } = null!;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PricePerNight { get; set; }
    }
}
