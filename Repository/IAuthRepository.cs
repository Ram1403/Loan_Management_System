using Loan_Management_System.Models;

namespace Loan_Management_System.Repository
{
    public interface IAuthRepository
    {
        LoginResponseViewModel Login(LoginViewModel login);
    }
}
