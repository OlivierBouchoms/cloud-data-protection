using System;

namespace CloudDataProtection.Services.MailService.Messaging.Dto
{
    public class EmailChangeRequestedMessage
    {
        public string NewEmail { get; set; }
        
        public string Url { get; set; }
        
        public DateTime ExpiresAt { get; set; }
    }
}