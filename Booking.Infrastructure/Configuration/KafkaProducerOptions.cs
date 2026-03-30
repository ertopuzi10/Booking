namespace Booking.Infrastructure.Configuration
{
    public class KafkaProducerOptions
    {
        public string BootstrapServers { get; set; } = string.Empty;
        public string ErrorTopic { get; set; } = "booking-logs";
    }
}
