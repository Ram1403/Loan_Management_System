using Loan_Management_System.Models;

namespace Loan_Management_System.Repository
{
    public interface ILoanSchemeRepository
    {
        Task<IEnumerable<LoanScheme>> GetAllAsync();
        Task<LoanScheme?> GetByIdAsync(int id);
        Task<LoanScheme> CreateAsync(LoanScheme scheme);
        Task<LoanScheme?> UpdateAsync(LoanScheme scheme);
        Task<LoanScheme?> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }

}
