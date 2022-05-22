using System;

namespace CloudDataProtection.Functions.BackupDemo.Entities
{
    public class FileScanInfo
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string AnalysisId { get; set; }
                
        public string ResourceUrl { get; set; }
        
        public string WidgetUrl { get; set; }
        
        public FileScanDestination Destination { get; set; }
    }
}