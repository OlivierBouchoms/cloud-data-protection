using System;
using System.Collections.Generic;
using System.Linq;
using CloudDataProtection.Core.Entities;

namespace CloudDataProtection.Functions.BackupDemo.Entities
{
    public class File
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        
        public string DisplayName { get; set; }
        
        public long Bytes { get; set; }
        
        public string ContentType { get; set; }

        public List<FileDestinationInfo> UploadedTo { get; set; } = new(1);
        
        public string Checksum { get; set; }
        
        public ChecksumAlgorithm ChecksumAlgorithm { get; set; }
        
        public FileScanInfo ScanInfo { get; set; }

        public bool IsUploaded => UploadedTo.Any(u => u.UploadSuccess);

        public void AddDestination(FileDestinationInfo info)
        {
            UploadedTo.Add(info);
        }
    }
}