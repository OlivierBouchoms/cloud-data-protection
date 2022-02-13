namespace CloudDataProtection.Services.Onboarding.Messaging.Client.Dto
{
    public class GetUserEmailRpcInput
    {
        public long UserId { get; }

        public GetUserEmailRpcInput(long userId)
        {
            UserId = userId;
        }
    }
}