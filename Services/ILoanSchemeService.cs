using Loan_Management_System.Models;

namespace Loan_Management_System.Services
{
    public interface ILoanSchemeService
    {
        Task<IEnumerable<LoanScheme>> GetAllSchemesAsync();
        Task<LoanScheme?> GetSchemeByIdAsync(int id);
        Task<LoanScheme> CreateSchemeAsync(LoanScheme scheme);
        Task<LoanScheme?> UpdateSchemeAsync(LoanScheme scheme);
        Task<LoanScheme?> DeleteSchemeAsync(int id);
    }

}
