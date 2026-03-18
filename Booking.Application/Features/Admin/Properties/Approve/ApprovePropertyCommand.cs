using MediatR;

namespace Booking.Application.Features.Admin.Properties.Approve
{
    public class ApprovePropertyCommand : IRequest<Unit>
    {
        public Guid PropertyId { get; set; }
    }
}
