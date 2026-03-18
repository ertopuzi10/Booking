using MediatR;

namespace Booking.Application.Features.Admin.Properties.Reject
{
    public class RejectPropertyCommand : IRequest<Unit>
    {
        public Guid PropertyId { get; set; }
        public string? Reason { get; set; }
    }
}
