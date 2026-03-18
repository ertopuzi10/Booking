using MediatR;

namespace Booking.Application.Features.Properties.Delete
{
    public record DeletePropertyCommand(Guid Id) : IRequest<Unit>;
}