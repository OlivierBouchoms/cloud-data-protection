using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CloudDataProtection.Core.Environment;

namespace CloudDataProtection.Ocelot
{
    public class BackupDemoHandler : DelegatingHandler
    {
        private readonly string _functionsKey;

        public BackupDemoHandler()
        {
            _functionsKey = EnvironmentVariableHelper.GetEnvironmentVariable("CDP_GATEWAY_BACKUP_DEMO_API_KEY");
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("x-functions-key", _functionsKey);
            
            return base.SendAsync(request, cancellationToken);
        }
    }
}