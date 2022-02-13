using CloudDataProtection.Core.Messaging;
using CloudDataProtection.Core.Messaging.RabbitMq;
using CloudDataProtection.Messaging.Dto;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CloudDataProtection.Messaging.Publisher
{
    public class AdminRegisteredMessagePublisher : RabbitMqMessagePublisher<AdminRegisteredMessage>
    {
        public AdminRegisteredMessagePublisher(IOptions<RabbitMqConfiguration> options, ILogger<AdminRegisteredMessagePublisher> logger) : base(options, logger)
        {
        }

        protected override string RoutingKey => RoutingKeys.AdminRegistered;
    }
}