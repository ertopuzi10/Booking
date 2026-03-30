using System.Threading;
using System.Threading.Tasks;

namespace Booking.Application.Abstractions
{
    public interface IEventPublisher
    {
        Task PublishAsync(string topic, object message, CancellationToken cancellationToken = default);
    }
}
