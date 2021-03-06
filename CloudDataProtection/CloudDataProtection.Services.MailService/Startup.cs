using System;
using CloudDataProtection.Services.MailService.Business;
using CloudDataProtection.Services.MailService.Messaging.Listener;
using CloudDataProtection.Services.MailService.Sender;
using CloudDataProtection.Core.Messaging.RabbitMq;
using CloudDataProtection.Services.MailService.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CloudDataProtection.Services.MailService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RabbitMqConfiguration>(options => Configuration.GetSection("RabbitMq").Bind(options));
            
            services.AddHostedService<AdminRegisteredMessageListener>();
            services.AddHostedService<ClientRegisteredMessageListener>();
            services.AddHostedService<PasswordUpdatedMessageListener>();
            services.AddHostedService<GoogleAccountConnectedMessageListener>();
            services.AddHostedService<UserDeletionCompleteMessageListener>();
            services.AddHostedService<EmailChangeRequestedMessageListener>();
            
            services.AddSingleton<RegistrationMailLogic>();
            services.AddSingleton<AccountMailLogic>();
            
            MailOptions mailOptions = new MailOptions();
            
            Configuration.GetSection("Mail").Bind(mailOptions);

            services.Configure<MailOptions>(options => Configuration.GetSection("Mail").Bind(options));

            switch (mailOptions.MailProtocol)
            {
                case MailProtocol.Smtp:
                    services.Configure<SmtpOptions>(options => Configuration.GetSection("Smtp").Bind(options));
                    services.AddSingleton<IMailSender, SmtpMailSender>();
                    break;
                case MailProtocol.Sendgrid:
                    services.Configure<SendGridOptions>(options => Configuration.GetSection("Sendgrid").Bind(options));
                    services.AddSingleton<IMailSender, SendGridMailSender>();
                    break;
                default:
                    throw new ArgumentException($"Unknown mail protocol {mailOptions.MailProtocol}");
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}