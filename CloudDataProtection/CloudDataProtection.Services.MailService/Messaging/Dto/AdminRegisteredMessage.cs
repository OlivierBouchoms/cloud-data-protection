using System;

namespace CloudDataProtection.Services.MailService.Messaging.Dto
{
    public class AdminRegisteredMessage
    {
        public long Id { get; set; }
        
        public string Email { get; set; }
        
        public string Url { get; set; }
        
        public DateTime Expiration { get; set; }
    }
}