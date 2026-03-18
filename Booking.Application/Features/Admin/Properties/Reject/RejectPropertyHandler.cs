using Booking.Application.Abstractions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Booking.Application.Features.Admin.Properties.Reject
{
    public class RejectPropertyHandler : IRequestHandler<RejectPropertyCommand, Unit>
    {
        private readonly IPropertyRepository _propertyRepository;

        public RejectPropertyHandler(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<Unit> Handle(RejectPropertyCommand request, CancellationToken cancellationToken)
        {
            var property = await _propertyRepository.GetByIdAsync(request.PropertyId, cancellationToken);
            if (property == null)
                throw new KeyNotFoundException($"Property with id {request.PropertyId} not found.");

            property.IsApproved = false;
            property.IsActive = false;

            await _propertyRepository.UpdateAsync(property, cancellationToken);

            return Unit.Value;
        }
    }
}
