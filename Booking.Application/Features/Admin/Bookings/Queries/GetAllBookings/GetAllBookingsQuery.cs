using MediatR;
using System;
using System.Collections.Generic;

namespace Booking.Application.Features.Admin.Bookings.Queries.GetAllBookings
{
    public class GetAllBookingsQuery : IRequest<List<GetAllBookingsResult>>
    {
        public string? Status { get; set; }
    }

    public class GetAllBookingsResult
    {
        public Guid BookingId { get; set; }
        public string PropertyName { get; set; } = string.Empty;
        public string GuestName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
