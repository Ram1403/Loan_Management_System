using Loan_Management_System.Models;

public interface INotificationRepository
{
    Task<IEnumerable<EmailNotification>> GetAllAsync();
    Task<IEnumerable<EmailNotification>> GetByUserAsync(int userId);
    Task<EmailNotification?> GetByIdAsync(int id);
    Task<EmailNotification> CreateAsync(EmailNotification notification);
    Task<bool> MarkAsSentAsync(int id);
    Task<bool> DeleteAsync(int id);
}
