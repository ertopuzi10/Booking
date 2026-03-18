using MediatR;
using System;
using System.Collections.Generic;

namespace Booking.Application.Features.Admin.Properties.GetPending
{
    public class GetPendingPropertiesQuery : IRequest<List<GetPendingPropertiesResult>>
    {
    }

    public class GetPendingPropertiesResult
    {
        public Guid PropertyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PropertyType { get; set; } = string.Empty;
        public string OwnerName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
