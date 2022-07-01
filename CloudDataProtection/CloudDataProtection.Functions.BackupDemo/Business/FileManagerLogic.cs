using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CloudDataProtection.Core.Cryptography.Aes;
using CloudDataProtection.Core.Cryptography.Checksum;
using CloudDataProtection.Core.Result;
using CloudDataProtection.Functions.BackupDemo.Business.Result;
using CloudDataProtection.Functions.BackupDemo.Entities;
using CloudDataProtection.Functions.BackupDemo.Extensions;
using CloudDataProtection.Functions.BackupDemo.Repository;
using CloudDataProtection.Functions.BackupDemo.Service;
using CloudDataProtection.Functions.BackupDemo.Service.Scan;
using Microsoft.AspNetCore.Http;
using File = CloudDataProtection.Functions.BackupDemo.Entities.File;

namespace CloudDataProtection.Functions.BackupDemo.Business
{
    public class FileManagerLogic
    {
        private readonly IDataTransformer _transformer;
        private readonly IFileRepository _repository;
        private readonly IFileScanService _fileScanService;
        private readonly IChecksumCalculator _checksumCalculator;
        private readonly IEnumerable<IFileService> _fileServices;

        public IEnumerable<IFileService> FileServices => _fileServices.ToImmutableList();

        public FileManagerLogic(IEnumerable<IFileService> fileServices,
            IDataTransformer transformer,
            IFileRepository repository,
            IFileScanService fileScanService,
            IChecksumCalculator checksumCalculator)
        {
            _fileServices = fileServices;
            _transformer = transformer;
            _repository = repository;
            _fileScanService = fileScanService;
            _checksumCalculator = checksumCalculator;
        }

        public async Task<BusinessResult<File>> Upload(IFormFile input, ICollection<FileDestination> destinations, bool runScan)
        {
            string fileName = GenerateFileName();

            using (Stream inputStream = input.OpenReadStream())
            {
                using (Stream encryptedStream = _transformer.Encrypt(inputStream))
                {
                    inputStream.Position = 0;
                    
                    File file = new()
                    {
                        ContentType = input.ContentType,
                        DisplayName = input.FileName,
                        Bytes = input.Length,
                        ChecksumAlgorithm = _checksumCalculator.Algorithm,
                        Checksum = _checksumCalculator.Calculate(inputStream)
                    };

                    Task<GetWidgetResult> getWidgetTask = null;
                    GetFileResult getFileResult = null;

                    Task<UploadFileResult> uploadFileTask = null;
                    Task<ReAnalyseFileResult> reAnalyseFileTask = null;

                    if (runScan)
                    {
                        getWidgetTask = _fileScanService.GetWidget(file);
                        getFileResult = await _fileScanService.GetFile(file);

                        if (getFileResult == null)
                        {
                            // Upload the file if it doesn't exist
                            MemoryStream scanCopy = new();

                            await inputStream.CopyToAndSeekAsync(scanCopy);

                            uploadFileTask = _fileScanService.UploadFile(file, scanCopy);
                        }
                        else
                        {
                            reAnalyseFileTask = _fileScanService.ReAnalyzeFile(file);
                        }
                    }

                    foreach (FileDestination destination in destinations)
                    {
                        IFileService fileService = ResolveFileService(destination);

                        if (fileService == null)
                        {
                            continue;
                        }

                        FileDestinationInfo info = new(destination);

                        try
                        {
                            MemoryStream copy = new();

                            await encryptedStream.CopyToAndSeekAsync(copy);

#if DEBUG
                            Stopwatch stopwatch = Stopwatch.StartNew();
#endif
                            
                            Service.Result.UploadFileResult fileResult = await fileService.Upload(copy, fileName);

#if DEBUG
                            stopwatch.Stop();
                            Console.WriteLine($"Uploading file to {destination.ToString()} took {stopwatch.ElapsedMilliseconds} ms");
#endif
                            
                            info.UploadCompletedAt = DateTime.Now;
                            info.UploadSuccess = fileResult.Success;
                            info.FileId = fileResult.Id;
                        }
                        catch (Exception e)
                        {
                            info.UploadSuccess = false;
                            info.FileId = null;
                        }

                        file.AddDestination(info);
                    }

                    if (runScan)
                    {
                        await getWidgetTask;

                        if (uploadFileTask != null)
                        {
                            await uploadFileTask;
                        }

                        if (reAnalyseFileTask != null)
                        {
                            await reAnalyseFileTask;
                        }

                        file.ScanInfo = new()
                        {
                            AnalysisId = getFileResult == null ? uploadFileTask.Result.AnalysisId : reAnalyseFileTask.Result.AnalysisId,
                            ResourceUrl = getFileResult == null ? uploadFileTask.Result.Url : getFileResult.Url,
                            WidgetUrl = getWidgetTask.Result.Url,
                            Destination = _fileScanService.Destination
                        };
                    }

                    if (file.IsUploaded)
                    {
                        await _repository.Create(file);
                    }

                    return BusinessResult<File>.Ok(file);
                }
            }
        }

        public async Task<BusinessResult<File>> Get(Guid id)
        {
            File file = await _repository.Get(id);

            if (file == null)
            {
                return BusinessResult<File>.NotFound($"Could not find file with id = {id.ToString()}");
            }

            return BusinessResult<File>.Ok(file);
        }

        public async Task<BusinessResult<FileDownloadInfo>> Download(Guid id)
        {
            BusinessResult<File> result = await Get(id);

            if (!result.Success)
            {
                return BusinessResult<FileDownloadInfo>.Error("An unknown error occured while retrieving info of the file");
            }

            return await Download(result.Data);
        }

        private async Task<BusinessResult<FileDownloadInfo>> Download(File file)
        {
            bool downloaded = false;
            byte[] data = Array.Empty<byte>();
            FileDestination? downloadedFrom = null;

            for (int i = 0; i < file.UploadedTo.Count && !downloaded; i++)
            {
                FileDestinationInfo info = file.UploadedTo[i];

                try
                {
                    IFileService fileService = ResolveFileService(info.Destination);

                    if (fileService == null)
                    {
                        continue;
                    }

                    Stream response = await fileService.GetDownloadStream(info.FileId);

                    data = _transformer.Decrypt(response);
                }
                catch (Exception e)
                {
                    continue;
                }

                downloaded = data != null && data.Length > 0;

                if (downloaded)
                {
                    downloadedFrom = info.Destination;
                }
            }

            if (!downloaded)
            {
                return BusinessResult<FileDownloadInfo>.Error("An unknown error occured while attempting to download the file");
            }

            FileDownloadInfo downloadInfo = new()
            {
                Bytes = data,
                FileName = file.DisplayName,
                ContentType = file.ContentType,
                DownloadedFrom = downloadedFrom
            };

            return BusinessResult<FileDownloadInfo>.Ok(downloadInfo);
        }

        private string GenerateFileName()
        {
            return Path.GetRandomFileName().Split('.')[0];
        }

        private IFileService ResolveFileService(FileDestination destination)
        {
            return _fileServices.FirstOrDefault(fs => fs.Destination == destination);
        }
    }
}