using System;

namespace CloudDataProtection.Functions.BackupDemo.Entities
{
    public class FileScanInfo
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Id linking to the analysis of the file
        /// </summary>
        public string AnalysisId { get; set; }
        
        public DateTime AnalysedAt { get; set; }
                
        public string ResourceUrl { get; set; }
        
        public string WidgetUrl { get; set; }
        
        public FileScanDestination Destination { get; set; }
    }
}