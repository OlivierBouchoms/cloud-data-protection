﻿using System.Threading.Tasks;
using CloudDataProtection.Core.Messaging;
using CloudDataProtection.Services.MailService.Business;
using CloudDataProtection.Services.MailService.Dto;
using CloudDataProtection.Core.Messaging.RabbitMq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CloudDataProtection.Services.MailService.Messaging.Listener
{
    public class ClientRegisteredMessageListener : RabbitMqMessageListener<ClientRegisteredModel>
    {
        private readonly RegistrationMailLogic _logic;

        public ClientRegisteredMessageListener(IOptions<RabbitMqConfiguration> options, ILogger<ClientRegisteredMessageListener> logger, RegistrationMailLogic logic) : base(options, logger)
        {
            _logic = logic;
        }

        protected override string RoutingKey => RoutingKeys.ClientRegistered;
        protected override string Queue => "FE024009-9AC7-4F6C-B21A-02C07E06511B";

        public override async Task HandleMessage(ClientRegisteredModel model)
        {
            await _logic.SendClientRegistered(model.Email);
        }
    }
}