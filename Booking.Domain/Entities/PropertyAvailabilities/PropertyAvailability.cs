using System;

namespace Booking.Domain.Entities
{
    public class PropertyAvailability
    {
        public Guid Id { get; set; }

        public Guid PropertyId { get; set; }
        public Properties Property { get; set; } = null!;

        public DateTime BlockedDate { get; set; }
        public string? Reason { get; set; }
    }
}
