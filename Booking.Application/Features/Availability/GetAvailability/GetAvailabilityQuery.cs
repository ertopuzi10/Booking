using MediatR;

namespace Booking.Application.Features.Availability.GetAvailability
{
    public record GetAvailabilityQuery(Guid PropertyId, int Year, int Month) : IRequest<GetAvailabilityDto>;
}
