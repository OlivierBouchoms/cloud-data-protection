using System;

namespace CloudDataProtection.Services.MailService.Dto
{
    public class ResetPasswordModel
    {
        public long Id { get; set; }
        
        public string Email { get; set; }
        
        public string Url { get; set; }
        
        public DateTime Expiration { get; set; }
    }
}