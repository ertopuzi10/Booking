using System;
using MediatR;

namespace Booking.Application.Features.Reviews.Commands.SubmitReview
{
    public class SubmitReviewCommand : IRequest<Guid>
    {
        public Guid BookingId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}
