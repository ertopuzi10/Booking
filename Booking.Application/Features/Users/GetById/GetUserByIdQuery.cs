using MediatR;

namespace Booking.Application.Features.Users.GetById
{
    public record GetUserByIdQuery(Guid Id) : IRequest<GetUserByIdDto>;
}
