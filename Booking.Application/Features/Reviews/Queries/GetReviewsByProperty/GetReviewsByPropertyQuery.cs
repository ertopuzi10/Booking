using System.Collections.Generic;
using MediatR;

namespace Booking.Application.Features.Reviews.Queries.GetReviewsByProperty
{
    public class GetReviewsByPropertyQuery : IRequest<List<GetReviewsByPropertyDto>>
    {
        public Guid PropertyId { get; set; }
    }
}
