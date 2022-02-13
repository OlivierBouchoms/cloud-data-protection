using System;

namespace CloudDataProtection.Services.Subscription.Messaging.Dto
{
    public class UserDataDeletedMessage
    {
        public long UserId { get; set; }
        
        public DateTime StartedAt { get; set; }
        
        public DateTime CompletedAt { get; set; }

        public string Service => "BackupConfiguration";
    }
}