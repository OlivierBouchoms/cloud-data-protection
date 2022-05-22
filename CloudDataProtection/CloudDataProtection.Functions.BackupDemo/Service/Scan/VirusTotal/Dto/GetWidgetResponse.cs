using Newtonsoft.Json;

namespace CloudDataProtection.Functions.BackupDemo.Service.Scan.VirusTotal.Dto
{
    public class GetWidgetResponse
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("found")]
        public bool Found { get; set; }

        [JsonProperty("detection_ratio")]
        public DetectionRatio DetectionRatio { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class DetectionRatio
    {
        [JsonProperty("detections")]
        public int Detections { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }
}