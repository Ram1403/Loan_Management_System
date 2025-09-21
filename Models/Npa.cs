using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;

namespace Loan_Management_System.Models
{
    public enum NpaStatus
    {
        Standard,
        Substandard,
        Doubtful,
        Loss
    }
    public class Npa
    {
        [Key]
        public int NpaId { get; set; }

        [ForeignKey("Loan")]
        public int LoanId { get; set; }

        public int DaysOverdue { get; set; }

        [Column(TypeName = "decimal(15, 2)")]
        public decimal OverdueAmount { get; set; }

        public DateTime NpaDate { get; set; }

        public NpaStatus NpaStatus { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual Loan ?Loan { get; set; }
    }
}
