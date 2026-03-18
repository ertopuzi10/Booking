using MediatR;

namespace Booking.Application.Features.Users.DeleteUser
{
    public record DeleteUserCommand(Guid UserId) : IRequest<Unit>;
}
