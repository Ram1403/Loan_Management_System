using Loan_Management_System.Models;

namespace Loan_Management_System.Repository
{
    public interface INpaRepository
    {
        Task<IEnumerable<Npa>> GetAllAsync();
        Task<IEnumerable<Npa>> GetByLoanIdAsync(int loanId);
        Task<Npa> FlagAsNpaAsync(Npa npa);
        Task<bool> DeleteAsync(int id);   // ✅ new

    }
}
