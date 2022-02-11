using System.Threading.Tasks;
using CloudDataProtection.Core.Messaging;
using CloudDataProtection.Core.Messaging.RabbitMq;
using CloudDataProtection.Services.Onboarding.Business;
using CloudDataProtection.Services.Onboarding.Dto;
using CloudDataProtection.Services.Onboarding.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CloudDataProtection.Services.Onboarding.Messaging.Listener
{
    public class ClientRegisteredMessageListener : RabbitMqMessageListener<UserRegisteredModel>
    {
        private readonly OnboardingBusinessLogic _logic;

        public ClientRegisteredMessageListener(IOptions<RabbitMqConfiguration> options, ILogger<ClientRegisteredMessageListener> logger, OnboardingBusinessLogic logic) :
            base(options, logger)
        {
            _logic = logic;
        }

        protected override string RoutingKey => RoutingKeys.ClientRegistered;
        
        public override async Task HandleMessage(UserRegisteredModel model)
        {
            Entities.Onboarding onboarding = new Entities.Onboarding
            {
                Status = OnboardingStatus.None,
                UserId = model.Id
            };

            await _logic.Create(onboarding);
        }
    }
}