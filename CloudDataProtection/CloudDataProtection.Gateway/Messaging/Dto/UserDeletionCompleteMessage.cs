namespace CloudDataProtection.Messaging.Dto
{
    public class UserDeletionCompleteMessage
    {
        public long UserId { get; set; }

        public string Email { get; set; }
    }
}