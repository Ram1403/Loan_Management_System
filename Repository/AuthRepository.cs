using Loan_Management_System.Models;
using Loan_Management_System.Data;

namespace Loan_Management_System.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly LoanDbContext _context;

        public AuthRepository(LoanDbContext context)
        {
            _context = context;
        }

        public LoginResponseViewModel Login(LoginViewModel login)
        {
            var user = _context.Users
      
                .FirstOrDefault(u => u.Username == login.Username && u.PasswordHash == login.PasswordHash);

            if (user != null)
            {
                return new LoginResponseViewModel
                {
                    IsSuccess = true,
                    User = user,
                    Token = "", 
                    Message = "Login successful"
                };
            }

            return new LoginResponseViewModel
            {
                IsSuccess = false,
                User = null,
                Token = "",
                Message = "Login failed. Please provide valid credentials."
            };
        }
    }
}
