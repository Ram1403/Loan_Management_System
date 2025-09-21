using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace Loan_Management_System.Models
{
    public enum Gender
    {
        Male,
        Female,
        Other
    }

    public enum DocumentType
    {
        Pan,
        Aadhaar,
        Passport,
        DrivingLicense
    }

    public enum VerificationStatus
    {
        Pending,
        Verified,
        Rejected
    }

    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(255, ErrorMessage = "Address cannot exceed 255 characters.")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Credit score is required.")]
        [Range(300, 900, ErrorMessage = "Credit score must be between 300 and 900.")]
        public int? CreditScore { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Verified At")]
        public DateTime? VerifiedAt { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [DataType(DataType.DateTime)]
        public DateTime? UpdatedAt { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public Gender? Gender { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(50, ErrorMessage = "City cannot exceed 50 characters.")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Occupation is required.")]
        [StringLength(100, ErrorMessage = "Occupation cannot exceed 100 characters.")]
        public string? Occupation { get; set; }

        [Required(ErrorMessage = "Annual income is required.")]
        [Column(TypeName = "decimal(15, 2)")]
        public decimal? AnnualIncome { get; set; }

        [Required(ErrorMessage = "PAN number is required.")]
        [StringLength(10, ErrorMessage = "PAN number must be 10 characters.")]
        public string? PanNumber { get; set; }

        [Required(ErrorMessage = "Aadhaar number is required.")]
        [StringLength(12, ErrorMessage = "Aadhaar number must be 12 characters.")]
        public string? AadhaarNumber { get; set; }

        [Required(ErrorMessage = "Document type is required.")]
        [EnumDataType(typeof(DocumentType))]
        public DocumentType? DocumentType { get; set; }

        [Required(ErrorMessage = "Document path is required.")]
        [StringLength(255, ErrorMessage = "Document path cannot exceed 255 characters.")]
        public string? DocumentPath { get; set; }

        [Required(ErrorMessage = "Verification status is required.")]
        [EnumDataType(typeof(VerificationStatus))]
        public VerificationStatus? VerificationStatus { get; set; }
        // Navigation Property for the relationship
        [ForeignKey("UserId")]
        public virtual User ?User { get; set; }
        [JsonIgnore]
        public virtual ICollection<LoanApplication>? LoanApplications { get; set; }
        [JsonIgnore]
        public virtual ICollection<CustomerQuery> ?CustomerQueries { get; set; }
    }
}
