using Loan_Management_System.Data;
using Loan_Management_System.Models;
using Loan_Management_System.Repository;
using Loan_Management_System.Services.Email;

namespace Loan_Management_System.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repo;
        private readonly IEmailSender _sender;

        public NotificationService(INotificationRepository repo, IEmailSender sender)
        {
            _repo = repo;
            _sender = sender;
        }

        public Task<IEnumerable<EmailNotification>> GetAllAsync() => _repo.GetAllAsync();
        public Task<IEnumerable<EmailNotification>> GetByUserAsync(int userId) => _repo.GetByUserAsync(userId);

        public async Task<EmailNotification> SendNotificationAsync(EmailNotification notification)
        {
            var saved = await _repo.CreateAsync(notification);

            // ✅ Load user email explicitly
            string recipient = "";
            if (saved.User == null)
            {
                using var scope = new LoanDbContext(); // or inject context properly
                var user = await scope.Users.FindAsync(saved.UserId);
                recipient = user?.Email ?? "";
            }
            else
            {
                recipient = saved.User.Email;
            }

            // ✅ Send
            var sent = await _sender.SendEmailAsync(recipient, saved.Subject, saved.Message);
            if (sent) await _repo.MarkAsSentAsync(saved.NotificationId);

            return saved;
        }


        public async Task<bool> SendNotificationByIdAsync(int id)
        {
            var n = await _repo.GetByIdAsync(id);
            if (n == null) return false;
            var recipient = n.User?.Email ?? ""; // or lookup user by id
            var sent = await _sender.SendEmailAsync(recipient, n.Subject, n.Message);
            if (sent) return await _repo.MarkAsSentAsync(id);
            return false;
        }

        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}
