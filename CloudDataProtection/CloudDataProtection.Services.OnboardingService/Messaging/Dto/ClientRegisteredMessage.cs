using System;

namespace CloudDataProtection.Services.Onboarding.Messaging.Dto
{
    public class ClientRegisteredMessage
    {
        public long Id { get; set; }
        
        public string Email { get; set; }
        
        [Obsolete]
        public UserRegisteredRole Role { get; set; }
    }

    [Obsolete]
    public enum UserRegisteredRole
    {
        Client = 0,
        Employee = 1,
        Admin = 2
    }
}