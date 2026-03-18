using Booking.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Booking.Application.Features.Admin.Bookings.Queries.GetAllBookings
{
    public class GetAllBookingsHandler : IRequestHandler<GetAllBookingsQuery, List<GetAllBookingsResult>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllBookingsHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetAllBookingsResult>> Handle(GetAllBookingsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.BookingsQuery
                .Include(b => b.Property)
                .Include(b => b.Guest)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.Status))
                query = query.Where(b => b.BookingStatus == request.Status);

            var results = await query
                .Select(b => new GetAllBookingsResult
                {
                    BookingId = b.Id,
                    PropertyName = b.Property.Name,
                    GuestName = b.Guest.FirstName + " " + b.Guest.LastName,
                    StartDate = b.StartDate,
                    EndDate = b.EndDate,
                    TotalPrice = b.TotalPrice,
                    Status = b.BookingStatus
                })
                .ToListAsync(cancellationToken);

            return results;
        }
    }
}
