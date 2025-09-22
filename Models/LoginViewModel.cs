using System.ComponentModel.DataAnnotations;

namespace Loan_Management_System.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage="user name is required")]
        public string Username { get; set; }
        [Required(ErrorMessage="password is required")]
        public string PasswordHash { get; set; }
    }
}
