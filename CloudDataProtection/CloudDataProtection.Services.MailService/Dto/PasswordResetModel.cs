namespace CloudDataProtection.Services.MailService.Dto
{
    public class PasswordResetModel
    {
        public string Email { get; set; }
        
        public long UserId { get; set; }
    }
}