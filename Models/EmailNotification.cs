using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loan_Management_System.Models
{
    public enum NotificationType
    {
        Reminder,
        Approval,
        Rejection,
        Overdue,
        General

    }
    public class EmailNotification
    {
        [Key]
        public int NotificationId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Loan")]
        public int? LoanId { get; set; }

        public NotificationType NotificationType { get; set; }

        [StringLength(200)]
        public string Subject { get; set; }

        public string Message { get; set; }

        [StringLength(100)]
        public string TemplateUsed { get; set; }

        public DateTime? ScheduledDate { get; set; }

        public bool IsSent { get; set; }

        public DateTime? SentAt { get; set; }

        public int RetryCount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual User? User { get; set; }
        public virtual Loan? Loan { get; set; }
    }
}
