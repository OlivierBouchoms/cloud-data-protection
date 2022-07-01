using System.IO;
using System.Net;
using System.Threading.Tasks;
using CloudDataProtection.Core.Environment;
using CloudDataProtection.Functions.BackupDemo.Entities;
using CloudDataProtection.Functions.BackupDemo.Extensions;
using CloudDataProtection.Functions.BackupDemo.Service.Scan.VirusTotal.Dto;
using Flurl;
using Flurl.Http;
using File = CloudDataProtection.Functions.BackupDemo.Entities.File;

namespace CloudDataProtection.Functions.BackupDemo.Service.Scan.VirusTotal
{
    /// <summary>
    /// Implementation of IFileScanService using the VirusTotal V3 API 
    /// </summary>
    public class VirusTotalFileScanService : IFileScanService
    {
        private readonly string _apiKey;

        private const string BaseUrl = "https://www.virustotal.com/api/v3";
        private IFlurlRequest RequestBase => new Url(BaseUrl).WithHeader("x-apikey", _apiKey).LogPerformance();
        
        public FileScanDestination Destination => FileScanDestination.VirusTotal;

        public VirusTotalFileScanService(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<GetWidgetResult> GetWidget(File file)
        {
            string endpoint = "/widget/url";

            IFlurlRequest request = RequestBase
                .AppendPathSegment(endpoint)
                .SetQueryParam("query", file.Checksum);
            
            VirusTotalResponse<GetWidgetResponse> response = await request.GetJsonAsync<VirusTotalResponse<GetWidgetResponse>>();

            return new GetWidgetResult(response.Data.Url);
        }

        public async Task<GetFileResult> GetFile(File file)
        {
            string endpoint = "/files";

            IFlurlRequest request = RequestBase.AppendPathSegment(endpoint).AppendPathSegment(file.Checksum).AllowHttpStatus(HttpStatusCode.NotFound);

            IFlurlResponse response = await request.GetAsync();
            
            bool fileExists = response.StatusCode == (int)HttpStatusCode.OK;

            if (!fileExists)
            {
                return new GetFileResult { FileExists = false };
            }
            
            VirusTotalResponse<GetFileResponse> body = await response.GetJsonAsync<VirusTotalResponse<GetFileResponse>>();

            return new GetFileResult
            {
                FileExists = true,
                CheckSum = body.Data.Id,
                Url = body.Data.Links.Self
            };
        }
        
        /// <summary>
        /// Request a file rescan (re-analyze)
        /// https://developers.virustotal.com/reference/files-analyse
        /// </summary>
        public async Task<ReAnalyseFileResult> ReAnalyzeFile(File file)
        {
            string endpoint = $"/files/{file.Checksum}/analyse";

            IFlurlRequest request = RequestBase.AppendPathSegment(endpoint);

            IFlurlResponse response = await request.PostAsync();
            
            VirusTotalResponse<UploadFileResponse> data = await response.GetJsonAsync<VirusTotalResponse<UploadFileResponse>>();

            return new ReAnalyseFileResult
            {
                AnalysisId = data.Data.Id,
                Url = string.Join("/", BaseUrl, "files", file.Checksum)
            };
        }

        /// <summary>
        /// Upload and analyse a file
        /// https://developers.virustotal.com/reference/files-scan
        /// </summary>
        public async Task<UploadFileResult> UploadFile(File file, Stream stream)
        {
            string endpoint = "/files";

            IFlurlRequest request = RequestBase.AppendPathSegment(endpoint);

            IFlurlResponse response = await request.PostMultipartAsync(c => c.AddFile("file", stream, file.DisplayName, file.ContentType));
            
            VirusTotalResponse<UploadFileResponse> data = await response.GetJsonAsync<VirusTotalResponse<UploadFileResponse>>();

            return new UploadFileResult
            {
                AnalysisId = data.Data.Id,
                Url = string.Join("/", BaseUrl, "files", file.Checksum)
            };
        }
    }
}