using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using CloudDataProtection.Core.Result;
using CloudDataProtection.Functions.BackupDemo.Authentication;
using CloudDataProtection.Functions.BackupDemo.Business;
using CloudDataProtection.Functions.BackupDemo.Entities;
using CloudDataProtection.Functions.BackupDemo.Extensions;
using CloudDataProtection.Functions.BackupDemo.Factory;
using CloudDataProtection.Functions.BackupDemo.Triggers.Dto.Input;
using CloudDataProtection.Functions.BackupDemo.Triggers.Dto.Output;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CloudDataProtection.Functions.BackupDemo.Triggers
{
    public static class FileUploadTrigger
    {
        [FunctionName("FileUpload")]
        public static async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post")]
            HttpRequest request, ILogger logger)
        {
            if (!request.HttpContext.IsAuthenticated())
            {
                return new UnauthorizedResult();
            }

            IFormFile file = request.Form.Files.FirstOrDefault();

            FileUploadInput input = request.FromFormData<FileUploadInput>("Input");
            
            if (file == null || input == null)
            { 
                return new BadRequestResult();
            }
            
            return await DoFileUpload(file, input);
        }

        private static async Task<IActionResult> DoFileUpload(IFormFile file, FileUploadInput input)
        {
            FileManagerLogic logic = FileManagerLogicFactory.Instance.GetLogic();

            BusinessResult<File> result = await logic.Upload(file, input.Destinations, input.RunScan);

            if (!result.Success || result.Data == null)
            {
                return new InternalServerErrorResult();
            }

            File entity = result.Data;

            FileUploadOutput output = new()
            {
                Id = entity.Id.ToString(),
                Bytes = entity.Bytes,
                ContentType = entity.ContentType,
                DisplayName = entity.DisplayName,
                UploadedTo = entity.UploadedTo
                    .Select(u => new FileUploadDestinationOutputEntry(u))
                    .ToList()
            };

            if (entity.ScanInfo != null)
            {
                output.ScanInfo = new ()
                {
                    Id = entity.Id,
                    WidgetUrl = entity.ScanInfo.WidgetUrl,
                    Destination = entity.ScanInfo.Destination.GetDescription()
                };
            }

            return new OkObjectResult(output);
        }
    }
}