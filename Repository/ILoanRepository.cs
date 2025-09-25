using Loan_Management_System.Models;

namespace Loan_Management_System.Repository
{
    public interface ILoanRepository
    {
        Task<IEnumerable<Loan>> GetAllAsync();
        Task<Loan?> GetByIdAsync(int id);
        Task<Loan> CreateAsync(Loan loan);
        Task<Loan?> UpdateAsync(Loan loan);
        Task<Loan?> TrackEmiAsync(int loanId, int emiPaid);
        Task<Loan?> DeleteAsync(int id);

    }

}
