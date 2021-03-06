using System.Threading.Tasks;
using CloudDataProtection.Core.DependencyInjection.Extensions;
using CloudDataProtection.Core.Papertrail.Extensions;
using CloudDataProtection.Data.Context;
using CloudDataProtection.Seeder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
                .ConfigureLogging(loggingBuilder => loggingBuilder.AddPapertrail())
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
                AdminSeeder service = scope.ServiceProvider.GetRequiredService<AdminSeeder>();

                Task task = service.Seed();
            
                task.Wait();
            }
            
            return webHost;
        }
    }
}