using System.Threading.Tasks;
using CloudDataProtection.Core.Messaging;
using CloudDataProtection.Core.Messaging.RabbitMq;
using CloudDataProtection.Services.MailService.Business;
using CloudDataProtection.Services.MailService.Dto;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CloudDataProtection.Services.MailService.Messaging.Listener
{
    public class AdminSeededMessageListener : RabbitMqMessageListener<AdminSeededModel>
    {
        private readonly RegistrationMailLogic _registrationMailLogic;

        public AdminSeededMessageListener(IOptions<RabbitMqConfiguration> options, ILogger<AdminSeededMessageListener> logger, RegistrationMailLogic registrationMailLogic) : base(options, logger)
        {
            _registrationMailLogic = registrationMailLogic;
        }

        protected override string RoutingKey => RoutingKeys.AdminSeeded;
        protected override string Queue => "84744B3F-604C-4EB9-B669-B819D1AA4557";
        public override async Task HandleMessage(AdminSeededModel model)
        {
            ResetPasswordModel resetPasswordModel = new ResetPasswordModel
            {
                Id = model.Id,
                Email = model.Email,
                Expiration = model.Expiration,
                Url = model.Url
            };
            
            await _registrationMailLogic.SendAdminRegistered(resetPasswordModel);
        }
    }
}