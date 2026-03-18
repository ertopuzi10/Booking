using Booking.Application.Abstractions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Booking.Application.Features.Admin.Bookings.Commands.Override
{
    public class OverrideBookingStatusHandler : IRequestHandler<OverrideBookingStatusCommand, Unit>
    {
        private readonly IBookingRepository _bookingRepository;

        public OverrideBookingStatusHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<Unit> Handle(OverrideBookingStatusCommand request, CancellationToken cancellationToken)
        {
            var booking = await _bookingRepository.GetByIdAsync(request.BookingId, cancellationToken)
                ?? throw new KeyNotFoundException($"Booking with id {request.BookingId} not found.");

            booking.BookingStatus = request.NewStatus;
            booking.LastModifiedAt = DateTime.UtcNow;

            await _bookingRepository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
