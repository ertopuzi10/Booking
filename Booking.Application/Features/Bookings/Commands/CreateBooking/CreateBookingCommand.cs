using System;
using MediatR;

namespace Booking.Application.Features.Bookings.Commands.CreateBooking
{
    public class CreateBookingCommand : IRequest<Guid>
    {
        public Guid PropertyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GuestCount { get; set; }
    }
}
