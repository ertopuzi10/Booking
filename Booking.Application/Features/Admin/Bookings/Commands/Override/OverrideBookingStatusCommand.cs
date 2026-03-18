using MediatR;

namespace Booking.Application.Features.Admin.Bookings.Commands.Override
{
    public class OverrideBookingStatusCommand : IRequest<Unit>
    {
        public Guid BookingId { get; set; }
        public string NewStatus { get; set; } = string.Empty;
    }
}
