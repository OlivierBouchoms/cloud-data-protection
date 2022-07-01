using Newtonsoft.Json;

namespace CloudDataProtection.Functions.BackupDemo.Service.Scan.VirusTotal.Dto
{
    public class UploadFileResponse
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}