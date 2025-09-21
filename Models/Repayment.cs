using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loan_Management_System.Models
{
    public enum PaymentStatus
    {
        Pending,
        Completed,
        Failed
    }
    public class Repayment
    {
        [Key]
        public int RepaymentId { get; set; }

        [ForeignKey("Loan")]
        public int LoanId { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }

        public int EmiNumber { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal PenaltyPaid { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal LateFee { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        public DateTime DueDate { get; set; }

        [StringLength(20)]
        public string PaymentMode { get; set; }

        [StringLength(100)]
        public string TransactionId { get; set; }

        public string? PaymentGatewayResponse { get; set; }

        public PaymentStatus? PaymentStatus { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual Loan ?Loan { get; set; }
    }
}
