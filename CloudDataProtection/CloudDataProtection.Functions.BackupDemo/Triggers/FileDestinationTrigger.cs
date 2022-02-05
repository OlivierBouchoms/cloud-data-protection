using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudDataProtection.Functions.BackupDemo.Authentication;
using CloudDataProtection.Functions.BackupDemo.Business;
using CloudDataProtection.Functions.BackupDemo.Extensions;
using CloudDataProtection.Functions.BackupDemo.Factory;
using CloudDataProtection.Functions.BackupDemo.Triggers.Dto.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace CloudDataProtection.Functions.BackupDemo.Triggers
{
    public static class FileDestinationTrigger
    {
        [FunctionName("FileDestination")]
        public static async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get")]
            HttpRequest request, ILogger logger)
        {
            if (!request.HttpContext.IsAuthenticated())
            {
                return new UnauthorizedResult();
            }
            
            FileManagerLogic logic = FileManagerLogicFactory.Instance.GetLogic();

            IEnumerable<FileDestinationResultEntry> sources = logic.FileServices
                .Select(fs => fs.Destination)
                .Select(d => new FileDestinationResultEntry((int) d, d.GetDescription()))
                .OrderBy(d => d.Description);

            FileDestinationResult result = new()
            {
                Sources = sources
            };

            return new OkObjectResult(result);
        }
    }
}