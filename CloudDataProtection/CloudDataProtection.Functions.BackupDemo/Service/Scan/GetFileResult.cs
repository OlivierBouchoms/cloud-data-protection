namespace CloudDataProtection.Functions.BackupDemo.Service.Scan
{
    public class GetFileResult
    {
        public bool FileExists { get; set; }
        
        public string CheckSum { get; set; }
        
        public string Url { get; set; }
    }
}