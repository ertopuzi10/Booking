using Booking.Application.Features.Availability.BlockDates;
using Booking.Application.Features.Availability.UnblockDates;
using Booking.Application.Features.Availability.SetSeasonalPrice;
using Booking.Application.Features.Availability.GetAvailability;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Booking.API.Controllers
{
    [Route("api/availability")]
    [ApiController]
    public class AvailabilityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AvailabilityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{propertyId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAvailability(Guid propertyId, [FromQuery] int year, [FromQuery] int month)
        {
            var result = await _mediator.Send(new GetAvailabilityQuery(propertyId, year, month));
            return Ok(result);
        }

        [HttpPost("{propertyId}/block")]
        [Authorize(Roles = "Host,Admin")]
        public async Task<IActionResult> BlockDates(Guid propertyId, [FromBody] BlockDatesRequest request)
        {
            var command = new BlockDatesCommand
            {
                PropertyId = propertyId,
                Dates = request.Dates,
                Reason = request.Reason
            };

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{propertyId}/unblock")]
        [Authorize(Roles = "Host,Admin")]
        public async Task<IActionResult> UnblockDates(Guid propertyId, [FromBody] UnblockDatesRequest request)
        {
            var command = new UnblockDatesCommand
            {
                PropertyId = propertyId,
                Dates = request.Dates
            };

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost("{propertyId}/seasonal-price")]
        [Authorize(Roles = "Host,Admin")]
        public async Task<IActionResult> SetSeasonalPrice(Guid propertyId, [FromBody] SetSeasonalPriceCommand command)
        {
            command.PropertyId = propertyId;
            await _mediator.Send(command);
            return NoContent();
        }
    }

    public class BlockDatesRequest
    {
        public System.Collections.Generic.List<System.DateTime> Dates { get; set; } = new();
        public string? Reason { get; set; }
    }

    public class UnblockDatesRequest
    {
        public System.Collections.Generic.List<System.DateTime> Dates { get; set; } = new();
    }
}
