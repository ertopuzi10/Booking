using System;

namespace Booking.Domain.Entities
{
    public class Reviews
    {
        public Guid Id { get; set; }

        public Guid BookingId { get; set; }
        public Bookings Bookings { get; set; } = null!;

        public Guid PropertyId { get; set; }
        public Properties Property { get; set; } = null!;

        public Guid GuestId { get; set; }
        public Users Guest { get; set; } = null!;

        public int Rating { get; set; }
        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
