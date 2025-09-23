using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Loan_Management_System.Models
{
    public class LoanOfficer
    {
        [Key]
        public int OfficerId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string Designation { get; set; }

        public int MaxLoansAssigned { get; set; }

        public int CurrentWorkload { get; set; }

        [StringLength(100)]
        public string Specialization { get; set; }

        [Column(TypeName = "decimal(3, 2)")]
        public decimal? PerformanceRating { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual User? User { get; set; }
        //public virtual LoanAdmin ?LoanAdmin { get; set; }
        [JsonIgnore]
        public virtual ICollection<LoanApplication> ?AssignedLoanApplications { get; set; }
        [JsonIgnore]
        public virtual ICollection<LoanDocument>? VerifiedLoanDocuments { get; set; }
        [JsonIgnore]
        public virtual ICollection<CustomerQuery> ?HandledCustomerQueries { get; set; }
        //[JsonIgnore]
        //public virtual ICollection<LoanAdmin>? LoanAdmins { get; set; }

    }
}
