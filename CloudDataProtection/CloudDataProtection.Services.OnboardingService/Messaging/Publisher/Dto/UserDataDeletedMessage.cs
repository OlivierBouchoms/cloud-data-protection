using System;

namespace CloudDataProtection.Services.Onboarding.Messaging.Publisher.Dto
{
    public class UserDataDeletedMessage
    {
        public long UserId { get; set; }
        
        public DateTime StartedAt { get; set; }
        
        public DateTime CompletedAt { get; set; }

        public string Service => "Onboarding";
    }
}