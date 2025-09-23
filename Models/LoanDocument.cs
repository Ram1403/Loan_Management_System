using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loan_Management_System.Models
{
    public class LoanDocument
    {
        [Key]
        public int DocumentId { get; set; }

        [ForeignKey("LoanApplication")]
        public int ApplicationId { get; set; }

        [ForeignKey("LoanOfficer")]
        public int? VerifiedBy { get; set; }

        [StringLength(50)]
        public string DocumentType { get; set; }

        [StringLength(255)]
        public string FilePath { get; set; } //clodinary url will be stored here

        [StringLength(100)]
        public string FileName { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        public Status VerificationStatus { get; set; } = Status.Pending;

        public DateTime? VerificationDate { get; set; }

        public string VerificationRemarks { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual LoanApplication ?LoanApplication { get; set; }
        public virtual LoanOfficer? VerifiedByOfficer { get; set; }
    }
}
