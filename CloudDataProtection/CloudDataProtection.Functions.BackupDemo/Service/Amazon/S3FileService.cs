using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CloudDataProtection.Functions.BackupDemo.Service.Result;

namespace CloudDataProtection.Functions.BackupDemo.Service.Amazon
{
    public class S3FileService : IS3FileService
    {
        public Task<UploadFileResult> Upload(Stream stream, string uploadFileName, IDictionary<string, string> tags)
        {
            throw new System.NotImplementedException();
        }

        public Task<InfoResult> GetInfo(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Stream> Download(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}