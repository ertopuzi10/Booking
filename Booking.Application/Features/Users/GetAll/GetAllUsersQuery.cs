using MediatR;
using System.Collections.Generic;

namespace Booking.Application.Features.Users.GetAll
{
    public record GetAllUsersQuery : IRequest<List<GetAllUsersDto>>;

    public class GetAllUsersDto
    {
        public Guid Id { get; init; }
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
        public string Username { get; init; } = null!;
        public string Email { get; init; } = null!;
        public bool IsActive { get; init; }
        public bool IsSuspended { get; init; }
    }
}
