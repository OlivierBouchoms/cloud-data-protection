using Newtonsoft.Json;

namespace CloudDataProtection.Functions.BackupDemo.Service.Scan.VirusTotal.Dto
{
    public class VirusTotalResponse<TData>
    {
        [JsonProperty("data")]
        public TData Data { get; set; }
    }
}