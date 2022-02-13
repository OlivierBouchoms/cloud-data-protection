using CloudDataProtection.Core.Messaging;
using CloudDataProtection.Core.Messaging.RabbitMq;
using CloudDataProtection.Messaging.Dto;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CloudDataProtection.Messaging.Publisher
{
    public class ClientRegisteredMessagePublisher : RabbitMqMessagePublisher<ClientRegisteredMessage>
    {
        public ClientRegisteredMessagePublisher(IOptions<RabbitMqConfiguration> options, ILogger<ClientRegisteredMessagePublisher> logger) : base(options, logger)
        {
        }

        protected override string RoutingKey => RoutingKeys.ClientRegistered;
    }
}