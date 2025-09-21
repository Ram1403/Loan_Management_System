using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Loan_Management_System.Models
{
    public class LoanScheme
    {
        [Key]
        public int SchemeId { get; set; }

        [StringLength(100)]
        public string SchemeName { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal InterestRate { get; set; }

        [Column(TypeName = "decimal(15, 2)")]
        public decimal MaxAmount { get; set; }

        [Column(TypeName = "decimal(15, 2)")]
        public decimal MinAmount { get; set; }

        public int TenureMonths { get; set; }

        public string EligibilityCriteria { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("LoanAdmin")]
        public int CreatedBy { get; set; }

        // Navigation properties
        public virtual LoanAdmin? LoanAdmin { get; set; }
        [JsonIgnore]
        public virtual ICollection<LoanApplication> ?LoanApplications { get; set; }
    }
}
