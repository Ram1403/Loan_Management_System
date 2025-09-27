using Loan_Management_System.Models;

namespace Loan_Management_System.Repository
{
    public interface ILoanSchemeRepository
    {
        Task<IEnumerable<LoanScheme>> GetAllSchemesAsync();
        Task<LoanScheme?> GetSchemeByIdAsync(int id);
        Task<LoanScheme> CreateSchemeAsync(LoanScheme scheme);
        Task<LoanScheme?> UpdateSchemeAsync(LoanScheme scheme);
        Task<bool> DeleteSchemeAsync(int id);
    }
}
