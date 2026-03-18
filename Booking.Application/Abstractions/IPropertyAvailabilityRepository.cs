namespace Booking.Application.Abstractions
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Booking.Domain.Entities;

    public interface IPropertyAvailabilityRepository
    {
        Task<List<PropertyAvailability>> GetByPropertyAndMonthAsync(Guid propertyId, int year, int month, CancellationToken cancellationToken);
        Task<List<PropertyAvailability>> GetBlockedDatesAsync(Guid propertyId, IEnumerable<DateTime> dates, CancellationToken cancellationToken);
        Task AddRangeAsync(IEnumerable<PropertyAvailability> availabilities, CancellationToken cancellationToken);
        Task RemoveRangeAsync(IEnumerable<PropertyAvailability> availabilities, CancellationToken cancellationToken);
        Task AddOrUpdateSeasonalPriceAsync(PropertySeasonalPrice seasonalPrice, CancellationToken cancellationToken);
    }
}
