using CloudDataProtection.Core.Messaging;
using CloudDataProtection.Core.Messaging.RabbitMq;
using CloudDataProtection.Dto.Result;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CloudDataProtection.Messaging.Publisher
{
    public class PasswordResetMessagePublisher : RabbitMqMessagePublisher<PasswordResetModel>
    {
        public PasswordResetMessagePublisher(IOptions<RabbitMqConfiguration> options, ILogger<PasswordResetMessagePublisher> logger) : base(options, logger)
        {
        }

        protected override string RoutingKey => RoutingKeys.PasswordReset;
    }
}