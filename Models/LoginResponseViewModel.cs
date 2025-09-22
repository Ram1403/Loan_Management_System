namespace Loan_Management_System.Models
{
    public class LoginResponseViewModel
    {
        public bool IsSuccess { get; set; }

        public User User { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
    }
}
