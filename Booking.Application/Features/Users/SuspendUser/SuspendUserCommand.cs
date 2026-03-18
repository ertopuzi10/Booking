using MediatR;

namespace Booking.Application.Features.Users.SuspendUser
{
    public class SuspendUserCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
    }
}
