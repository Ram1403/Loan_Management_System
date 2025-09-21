using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text.Json.Serialization;

namespace Loan_Management_System.Models
{ //if you remove thhsese then you willget 0 1 2 instead of string values in enum
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Role
    {
        Admin,
        LoanOfficer,
        Customer
    }
   
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password hash is required.")]
        [StringLength(255, ErrorMessage = "Password hash cannot exceed 255 characters.")]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number.")]
        [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public Role Role { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? UpdatedBy { get; set; }
        [DefaultValue(false)]

        public bool IsDeleted { get; set; }= false;

        public DateTime? DeletedAt { get; set; }
        // Navigation properties for one-to-one relationships
        [JsonIgnore]
        public virtual Customer? Customer { get; set; }
        [JsonIgnore]
        public virtual LoanOfficer? LoanOfficer { get; set; }
        [JsonIgnore]
        public virtual LoanAdmin ?LoanAdmin { get; set; }

        // Navigation properties for one-to-many relationships
        [JsonIgnore]
        public virtual ICollection<EmailNotification> ?EmailNotifications { get; set; }
        [JsonIgnore]
        public virtual ICollection<Report> ?Reports { get; set; }
    }
}
