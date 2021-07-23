namespace CloudDataProtection.Functions.BackupDemo.Service.Result
{
    public class UploadFileResult
    {
        public bool Success { get; }
        public string Id { get; set; }

        public UploadFileResult(bool success)
        {
            Success = success;
        }
    }
}