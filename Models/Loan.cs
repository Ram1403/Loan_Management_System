using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Loan_Management_System.Models
{
    public enum LoanStatus
    {
    
        Active,
        Completed,
        NPA,
        Closed

    }
    public class Loan
    {
        [Key]
        public int LoanId { get; set; }

        [ForeignKey("LoanApplication")]
        public int ApplicationId { get; set; }

        [Column(TypeName = "decimal(15, 2)")]
        public decimal SanctionedAmount { get; set; }

        [Column(TypeName = "decimal(15, 2)")]
        public decimal RemainingAmount { get; set; } //amount with interest remaining to be paid

        [Column(TypeName = "decimal(10, 2)")]
        public decimal EmiAmount { get; set; }

        public int TotalEmiCount { get; set; }

        public int PaidEmiCount { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal InterestRateApplied { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal PenaltyAmount { get; set; }

        public DateTime? LoanStartDate { get; set; }

        public DateTime? LoanEndDate { get; set; }

        public DateTime? DisbursementDate { get; set; }

        public DateTime? NextDueDate { get; set; }

        public LoanStatus LoanStatus { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        [JsonIgnore]
        public virtual LoanApplication? LoanApplication { get; set; }
        [JsonIgnore]
        public virtual ICollection<Repayment> ?Repayments { get; set; }
        [JsonIgnore]
        public virtual Npa ?Npa { get; set; }
        [JsonIgnore]
        public virtual ICollection<EmailNotification>? EmailNotifications { get; set; }
    }
}
