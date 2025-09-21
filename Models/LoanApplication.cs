using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Loan_Management_System.Models
{
    public enum Status
    {
        Pending,
        Approved,
        Rejected,
        Disbursed

    }
    public class LoanApplication
    {
        [Key]
        public int ApplicationId { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [ForeignKey("LoanScheme")]
        public int SchemeId { get; set; }

        [ForeignKey("LoanOfficer")]
        public int? OfficerId { get; set; }

        [Column(TypeName = "decimal(15, 2)")]
        public decimal RequestedAmount { get; set; }

        [StringLength(200)]
        public string PurposeOfLoan { get; set; }

        public string EmploymentDetails { get; set; }

        public string SubmittedDocuments { get; set; }

        public Status Status { get; set; }

        public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;

        public DateTime? OfficerAssignedDate { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public string? RejectionReason { get; set; }

        public string? Remarks { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual Customer? Customer { get; set; }
        public virtual LoanScheme? LoanScheme { get; set; }
        public virtual LoanOfficer ?LoanOfficer { get; set; }
        public virtual Loan? Loan { get; set; }
        [JsonIgnore]
        public virtual ICollection<LoanDocument>? LoanDocuments { get; set; }
    }
}
