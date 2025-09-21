using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Loan_Management_System.Models
{
    public class LoanAdmin
    {
        [Key]
        public int AdminId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [StringLength(20)]
        public string AdminLevel { get; set; }

        public DateTime? AppointedDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        //[JsonIgnore]
        public virtual User? User { get; set; }
        [JsonIgnore]
        public virtual ICollection<LoanScheme> ?LoanSchemes { get; set; }
        [JsonIgnore]
        public virtual ICollection<LoanOfficer> ?LoanOfficers { get; set; }
        [JsonIgnore]
        public virtual ICollection<Report> ?Reports { get; set; }
    }
}
