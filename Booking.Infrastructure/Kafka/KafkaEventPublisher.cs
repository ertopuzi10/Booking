using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Booking.Application.Abstractions;
using Booking.Infrastructure.Configuration;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Booking.Infrastructure.Kafka
{
    public class KafkaEventPublisher : IEventPublisher
    {
        private readonly KafkaProducerOptions _options;
        private readonly ILogger<KafkaEventPublisher> _logger;

        public KafkaEventPublisher(IOptions<KafkaProducerOptions> options, ILogger<KafkaEventPublisher> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        public async Task PublishAsync(string topic, object message, CancellationToken cancellationToken = default)
        {
            try
            {
                var json = JsonSerializer.Serialize(message);

                var config = new ProducerConfig
                {
                    BootstrapServers = _options.BootstrapServers,
                    MessageTimeoutMs = 3000,
                    SocketTimeoutMs = 3000,
                };

                using var producer = new ProducerBuilder<Null, string>(config).Build();

                await producer.ProduceAsync(topic, new Message<Null, string> { Value = json }, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to publish event to Kafka topic '{Topic}'", topic);
            }
        }
    }
}