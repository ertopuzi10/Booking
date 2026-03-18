using MediatR;

namespace Booking.Application.Features.Bookings.Commands.RejectBooking
{
    public class RejectBookingCommand : IRequest<Unit>
    {
        public Guid BookingId { get; set; }
        public string? Reason { get; set; }
    }
}
