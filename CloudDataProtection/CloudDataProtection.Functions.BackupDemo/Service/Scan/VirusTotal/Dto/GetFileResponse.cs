using System.Collections.Generic;
using Newtonsoft.Json;

namespace CloudDataProtection.Functions.BackupDemo.Service.Scan.VirusTotal.Dto
{
    public class GetFileResponse
    {
        [JsonProperty("attributes")]
        public Attributes Attributes { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("links")]
        public Links Links { get; set; }
    }

    public class Attributes
    {
        [JsonProperty("type_description")]
        public string TypeDescription { get; set; }

        [JsonProperty("tlsh")]
        public string Tlsh { get; set; }

        [JsonProperty("trid")]
        public List<Trid> Trid { get; set; }

        [JsonProperty("names")]
        public List<string> Names { get; set; }

        [JsonProperty("signature_info")]
        public SignatureInfo SignatureInfo { get; set; }

        [JsonProperty("last_modification_date")]
        public int LastModificationDate { get; set; }

        [JsonProperty("type_tag")]
        public string TypeTag { get; set; }

        [JsonProperty("times_submitted")]
        public int TimesSubmitted { get; set; }

        [JsonProperty("total_votes")]
        public TotalVotes TotalVotes { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("type_extension")]
        public string TypeExtension { get; set; }

        [JsonProperty("last_submission_date")]
        public int LastSubmissionDate { get; set; }

        [JsonProperty("known_distributors")]
        public KnownDistributors KnownDistributors { get; set; }

        [JsonProperty("sha256")]
        public string Sha256 { get; set; }

        [JsonProperty("trusted_verdict")]
        public TrustedVerdict TrustedVerdict { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }

        [JsonProperty("last_analysis_date")]
        public int LastAnalysisDate { get; set; }

        [JsonProperty("unique_sources")]
        public int UniqueSources { get; set; }

        [JsonProperty("first_submission_date")]
        public int FirstSubmissionDate { get; set; }

        [JsonProperty("ssdeep")]
        public string Ssdeep { get; set; }

        [JsonProperty("md5")]
        public string Md5 { get; set; }

        [JsonProperty("sha1")]
        public string Sha1 { get; set; }

        [JsonProperty("magic")]
        public string Magic { get; set; }

        [JsonProperty("last_analysis_stats")]
        public LastAnalysisStats LastAnalysisStats { get; set; }

        [JsonProperty("meaningful_name")]
        public string MeaningfulName { get; set; }

        [JsonProperty("reputation")]
        public int Reputation { get; set; }

        [JsonProperty("first_seen_itw_date")]
        public int FirstSeenItwDate { get; set; }

        [JsonProperty("monitor_info")]
        public MonitorInfo MonitorInfo { get; set; }
    }

    public class CounterSignersDetail
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("valid usage")]
        public string ValidUsage { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("algorithm")]
        public string Algorithm { get; set; }

        [JsonProperty("valid from")]
        public string ValidFrom { get; set; }

        [JsonProperty("valid to")]
        public string ValidTo { get; set; }

        [JsonProperty("serial number")]
        public string SerialNumber { get; set; }

        [JsonProperty("cert issuer")]
        public string CertIssuer { get; set; }

        [JsonProperty("thumbprint")]
        public string Thumbprint { get; set; }
    }
    
    public class KnownDistributors
    {
        [JsonProperty("filenames")]
        public List<string> Filenames { get; set; }

        [JsonProperty("products")]
        public List<string> Products { get; set; }

        [JsonProperty("distributors")]
        public List<string> Distributors { get; set; }

        [JsonProperty("data_sources")]
        public List<string> DataSources { get; set; }
    }

    public class LastAnalysisStats
    {
        [JsonProperty("harmless")]
        public int Harmless { get; set; }

        [JsonProperty("type-unsupported")]
        public int TypeUnsupported { get; set; }

        [JsonProperty("suspicious")]
        public int Suspicious { get; set; }

        [JsonProperty("confirmed-timeout")]
        public int ConfirmedTimeout { get; set; }

        [JsonProperty("timeout")]
        public int Timeout { get; set; }

        [JsonProperty("failure")]
        public int Failure { get; set; }

        [JsonProperty("malicious")]
        public int Malicious { get; set; }

        [JsonProperty("undetected")]
        public int Undetected { get; set; }
    }

    public class Links
    {
        [JsonProperty("self")]
        public string Self { get; set; }
    }

    public class MonitorInfo
    {
        [JsonProperty("organizations")]
        public List<string> Organizations { get; set; }

        [JsonProperty("filenames")]
        public List<string> Filenames { get; set; }
    }

    public class SignatureInfo
    {
        [JsonProperty("verified")]
        public string Verified { get; set; }

        [JsonProperty("signing date")]
        public string SigningDate { get; set; }

        [JsonProperty("signers")]
        public string Signers { get; set; }

        [JsonProperty("counter signers details")]
        public List<CounterSignersDetail> CounterSignersDetails { get; set; }

        [JsonProperty("counter signers")]
        public string CounterSigners { get; set; }

        [JsonProperty("signers details")]
        public List<SignersDetail> SignersDetails { get; set; }
    }

    public class SignersDetail
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("valid usage")]
        public string ValidUsage { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("algorithm")]
        public string Algorithm { get; set; }

        [JsonProperty("valid from")]
        public string ValidFrom { get; set; }

        [JsonProperty("valid to")]
        public string ValidTo { get; set; }

        [JsonProperty("serial number")]
        public string SerialNumber { get; set; }

        [JsonProperty("cert issuer")]
        public string CertIssuer { get; set; }

        [JsonProperty("thumbprint")]
        public string Thumbprint { get; set; }
    }

    public class TotalVotes
    {
        [JsonProperty("harmless")]
        public int Harmless { get; set; }

        [JsonProperty("malicious")]
        public int Malicious { get; set; }
    }

    public class Trid
    {
        [JsonProperty("file_type")]
        public string FileType { get; set; }

        [JsonProperty("probability")]
        public double Probability { get; set; }
    }

    public class TrustedVerdict
    {
        [JsonProperty("organization")]
        public string Organization { get; set; }

        [JsonProperty("verdict")]
        public string Verdict { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("filename")]
        public string Filename { get; set; }
    }
}