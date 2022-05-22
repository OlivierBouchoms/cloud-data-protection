using System.IO;
using System.Threading.Tasks;
using CloudDataProtection.Functions.BackupDemo.Entities;
using CloudDataProtection.Functions.BackupDemo.Service.Scan;
using File = CloudDataProtection.Functions.BackupDemo.Entities.File;

namespace CloudDataProtection.Functions.BackupDemo.Service
{
    public interface IFileScanService
    {
        Task<GetWidgetResult> GetWidget(File file);

        Task<GetFileResult> GetFile(File file);

        Task<UploadFileResult> UploadFile(File file, Stream stream);
        
        Task<ReAnalyseFileResult> ReAnalyzeFile(File file);

        FileScanDestination Destination { get; }
    }
}