using System;
using System.Collections.Generic;
using Flurl.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace CloudDataProtection.Functions.BackupDemo.Extensions
{
    public static class FlurlRequestExtensions
    {
        private const int PathLength = 64;
        
        public static IFlurlRequest LogPerformance(this IFlurlRequest request)
        {
            request.AfterCall(r =>
            {
                if (!r.Duration.HasValue)
                {
                    return;
                }
                
                string path = r.Request.Url.ToString();
                string url = path.Substring(0, Math.Min(PathLength, path.Length)).PadRight(PathLength);
                
                Console.WriteLine($"{r.Request.Verb}\t{url}\t{r.Duration.Value.TotalMilliseconds} ms");
            });

            return request;
        }
    }
}