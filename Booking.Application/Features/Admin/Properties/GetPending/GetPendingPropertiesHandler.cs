using Booking.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Booking.Application.Features.Admin.Properties.GetPending
{
    public class GetPendingPropertiesHandler : IRequestHandler<GetPendingPropertiesQuery, List<GetPendingPropertiesResult>>
    {
        private readonly IApplicationDbContext _context;

        public GetPendingPropertiesHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetPendingPropertiesResult>> Handle(GetPendingPropertiesQuery request, CancellationToken cancellationToken)
        {
            var results = await _context.PropertiesQuery
                .Where(p => !p.IsApproved)
                .Join(
                    _context.UsersQuery,
                    p => p.OwnerId,
                    u => u.Id,
                    (p, u) => new GetPendingPropertiesResult
                    {
                        PropertyId = p.Id,
                        Name = p.Name,
                        PropertyType = p.PropertyType,
                        OwnerName = u.FirstName + " " + u.LastName,
                        CreatedAt = p.CreatedAt
                    })
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return results;
        }
    }
}
