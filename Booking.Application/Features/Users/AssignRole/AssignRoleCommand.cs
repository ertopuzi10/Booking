using MediatR;

namespace Booking.Application.Features.Users.AssignRole
{
    public class AssignRoleCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
        public string RoleName { get; set; } = null!;
    }
}
