namespace CloudDataProtection.Services.Onboarding.Messaging.Publisher.Dto
{
    public class GoogleAccountConnectedMessage
    {
        public long UserId { get; }
        
        public string Email { get; }

        public GoogleAccountConnectedMessage(long userId, string email)
        {
            UserId = userId;
            Email = email;
        }
    }
}