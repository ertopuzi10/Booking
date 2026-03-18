using System;
using System.Collections.Generic;

namespace Booking.Application.Features.Users.GetById
{
    public class GetUserByIdDto
    {
        public Guid Id { get; init; }
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
        public string Username { get; init; } = null!;
        public string Email { get; init; } = null!;
        public string? PhoneNumber { get; init; }
        public string? ProfileImageUrl { get; init; }
        public bool IsActive { get; init; }
        public bool IsSuspended { get; init; }
        public DateTime CreatedAt { get; init; }
        public List<string> Roles { get; init; } = new List<string>();
    }
}
