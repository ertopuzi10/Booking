using MediatR;

namespace Booking.Application.Features.Bookings.Queries.GetBookingById
{
    public class GetBookingByIdQuery : IRequest<GetBookingByIdDto>
    {
        public Guid BookingId { get; set; }
    }
}
