using Loan_Management_System.Models;

namespace Loan_Management_System.Services
{
    public interface INotificationService
    {
        Task<IEnumerable<EmailNotification>> GetAllAsync();
        Task<IEnumerable<EmailNotification>> GetByUserAsync(int userId);
        Task<EmailNotification> SendNotificationAsync(EmailNotification notification);
        Task<bool> SendNotificationByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
