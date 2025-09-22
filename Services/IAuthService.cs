using Loan_Management_System.Models;

namespace Loan_Management_System.Services
{
    public interface IAuthService
    {
        LoginResponseViewModel Login(LoginViewModel login);
    }
}
