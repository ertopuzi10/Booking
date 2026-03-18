using Booking.Application.Features.Admin.Properties.Approve;
using Booking.Application.Features.Admin.Properties.Reject;
using Booking.Application.Features.Admin.Properties.GetPending;
using Booking.Application.Features.Admin.Bookings.Queries.GetAllBookings;
using Booking.Application.Features.Admin.Bookings.Commands.Override;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Booking.API.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("properties/pending")]
        public async Task<IActionResult> GetPendingProperties()
        {
            var result = await _mediator.Send(new GetPendingPropertiesQuery());
            return Ok(result);
        }

        [HttpPost("properties/{id}/approve")]
        public async Task<IActionResult> ApproveProperty(Guid id)
        {
            await _mediator.Send(new ApprovePropertyCommand { PropertyId = id });
            return NoContent();
        }

        [HttpPost("properties/{id}/reject")]
        public async Task<IActionResult> RejectProperty(Guid id, [FromBody] RejectPropertyRequest request)
        {
            await _mediator.Send(new RejectPropertyCommand { PropertyId = id, Reason = request?.Reason });
            return NoContent();
        }

        [HttpGet("bookings")]
        public async Task<IActionResult> GetAllBookings([FromQuery] string? status)
        {
            var result = await _mediator.Send(new GetAllBookingsQuery { Status = status });
            return Ok(result);
        }

        [HttpPut("bookings/{id}/status")]
        public async Task<IActionResult> OverrideBookingStatus(Guid id, [FromBody] OverrideBookingStatusRequest request)
        {
            await _mediator.Send(new OverrideBookingStatusCommand { BookingId = id, NewStatus = request.NewStatus });
            return NoContent();
        }
    }

    public class RejectPropertyRequest
    {
        public string? Reason { get; set; }
    }

    public class OverrideBookingStatusRequest
    {
        public string NewStatus { get; set; } = string.Empty;
    }
}
