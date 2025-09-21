using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loan_Management_System.Models
{
    public class Report
    {
        [Key]
        public int ReportId { get; set; }

        [ForeignKey("LoanAdmin")]
        public int GeneratedBy { get; set; }

        [StringLength(50)]
        public string ReportType { get; set; }

        public DateTime GeneratedDate { get; set; }

        public string Parameters { get; set; }

        [StringLength(255)]
        public string FilePath { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual LoanAdmin ?LoanAdmin { get; set; }
    }
}
