using Loan_Management_System.Models;

namespace Loan_Management_System.Services
{
    public interface ILoanService
    {
        Task<IEnumerable<Loan>> GetAllLoansAsync();
        Task<Loan?> GetLoanByIdAsync(int id);
        Task<Loan> CreateLoanAsync(Loan loan);
        Task<Loan?> UpdateLoanAsync(Loan loan);
        Task<Loan?> TrackEmiAsync(int loanId, int emiPaid);
        Task<Loan?> DeleteLoanAsync(int id);

    }

}
