namespace Loan_Management_System.Services.Email
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(string to, string subject, string body);
    }
}
