using CloudDataProtection.Core.Messaging;
using CloudDataProtection.Core.Messaging.RabbitMq;
using CloudDataProtection.Dto.Result;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CloudDataProtection.Messaging.Publisher
{
    public class AdminSeededMessagePublisher : RabbitMqMessagePublisher<AdminSeededModel>
    {
        public AdminSeededMessagePublisher(IOptions<RabbitMqConfiguration> options, ILogger<AdminSeededMessagePublisher> logger) : base(options, logger)
        {
        }

        protected override string RoutingKey => RoutingKeys.AdminSeeded;
    }
}