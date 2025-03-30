
using System.Net;
using System.Net.Mail;

namespace IOT.Services
{
    public class MailService : IMailService
    {
        public async Task<bool> SendEmailAsync(string recipientEmail, string subject, string body)
        {
            var senderEmail = "zozz70719@gmail.com";
            var appPassword = "psgsbqqzrujsbyfg"; // From Google

            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(senderEmail, appPassword),
                    EnableSsl = true,
                    UseDefaultCredentials = false
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(senderEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(recipientEmail);

                await smtpClient.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}

