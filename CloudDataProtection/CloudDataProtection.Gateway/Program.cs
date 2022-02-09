using System.Threading.Tasks;
using CloudDataProtection.Business;
using CloudDataProtection.Business.Options;
using CloudDataProtection.Core.DependencyInjection.Extensions;
using CloudDataProtection.Core.Messaging;
using CloudDataProtection.Core.Papertrail.Extensions;
using CloudDataProtection.Data.Context;
using CloudDataProtection.Dto.Result;
using CloudDataProtection.Seeder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace CloudDataProtection
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .Migrate<IAuthenticationDbContext, AuthenticationDbContext>()
                .Seed()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            IHostBuilder builder = Host.CreateDefaultBuilder(args);

            return builder
                .ConfigureServices(s => s.AddSingleton(builder))
                .ConfigureAppConfiguration((context, config) => config.AddJsonFile($"ocelot.{context.HostingEnvironment.EnvironmentName}.json"))
                .ConfigureLogging(loggingBuilder => loggingBuilder.ConfigureLogging())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
    }

    public static class WebHostExtensions
    {
        public static IHost Seed(this IHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                AuthenticationBusinessLogic authenticationBusinessLogic = scope.ServiceProvider.GetService<AuthenticationBusinessLogic>();
                IMessagePublisher<AdminSeededModel> publisher = scope.ServiceProvider.GetService<IMessagePublisher<AdminSeededModel>>();
                IOptions<AdminSeederOptions> options = scope.ServiceProvider.GetService<IOptions<AdminSeederOptions>>();
                IOptions<ResetPasswordOptions> resetPasswordOptions = scope.ServiceProvider.GetService<IOptions<ResetPasswordOptions>>();

                AdminSeeder service = new AdminSeeder(authenticationBusinessLogic, publisher, options, resetPasswordOptions);

                Task task = service.Seed();
            
                task.Wait();
            }
            
            return webHost;
        }
    }
}