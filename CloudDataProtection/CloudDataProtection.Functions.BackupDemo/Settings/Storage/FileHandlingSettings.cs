using CloudDataProtection.Core.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CloudDataProtection.Functions.BackupDemo.Settings.Storage
{
    public class FileHandlingSettings
    {
        [JsonProperty(PropertyName = "ChecksumAlgorithm", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public ChecksumAlgorithm ChecksumAlgorithm { get; set; }
    }
}