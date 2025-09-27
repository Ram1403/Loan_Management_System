using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using Loan_Management_System.Configurations;

namespace Loan_Management_System.Services.Email
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly EmailSettings _settings;
        public SmtpEmailSender(IOptions<EmailSettings> options) => _settings = options.Value;

        public async Task<bool> SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                using var client = new SmtpClient(_settings.SmtpServer, _settings.Port)
                {
                    Credentials = new NetworkCredential(_settings.Username, _settings.Password),
                    EnableSsl = true
                };

                var msg = new MailMessage
                {
                    From = new MailAddress(_settings.SenderEmail, _settings.SenderName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                msg.To.Add(to);

                await client.SendMailAsync(msg);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SMTP ERROR] {ex.Message}");
                throw;
            }
        }
    }
}
