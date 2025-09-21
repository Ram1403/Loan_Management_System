using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loan_Management_System.Models
{
    public enum QueryStatus
    {
        Open,
        InProgress,
        Resolved,
        Closed
    }
    public class CustomerQuery
    {
        [Key]
        public int QueryId { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [ForeignKey("LoanOfficer")]
        public int? OfficerId { get; set; }

        [StringLength(200)]
        public string QuerySubject { get; set; }

        public string QueryDescription { get; set; }

        public QueryStatus QueryStatus { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ResolvedAt { get; set; }

        public string OfficerResponse { get; set; }

        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual Customer? Customer { get; set; }
        public virtual LoanOfficer? LoanOfficer { get; set; }
    }
}
