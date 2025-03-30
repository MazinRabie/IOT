namespace IOT.Services
{
    public interface IMailService
    {
        Task<bool> SendEmailAsync(string recipientEmail, string subject, string body);
    }
}
