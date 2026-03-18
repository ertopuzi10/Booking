using MediatR;

namespace Booking.Application.Features.Bookings.Commands.ConfirmBooking
{
    public class ConfirmBookingCommand : IRequest<Unit>
    {
        public Guid BookingId { get; set; }
    }
}
