using Booking.Application.Abstractions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Booking.Application.Features.Admin.Properties.Approve
{
    public class ApprovePropertyHandler : IRequestHandler<ApprovePropertyCommand, Unit>
    {
        private readonly IPropertyRepository _propertyRepository;

        public ApprovePropertyHandler(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<Unit> Handle(ApprovePropertyCommand request, CancellationToken cancellationToken)
        {
            var property = await _propertyRepository.GetByIdAsync(request.PropertyId, cancellationToken);
            if (property == null)
                throw new KeyNotFoundException($"Property with id {request.PropertyId} not found.");

            property.IsApproved = true;
            property.IsActive = true;

            await _propertyRepository.UpdateAsync(property, cancellationToken);

            return Unit.Value;
        }
    }
}
