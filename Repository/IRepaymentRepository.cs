using Loan_Management_System.Models;

namespace Loan_Management_System.Repository
{
    public interface IRepaymentRepository
    {
        Task<IEnumerable<Repayment>> GetAllAsync();
        Task<Repayment?> GetByIdAsync(int id);
        Task<IEnumerable<Repayment>> GetByLoanIdAsync(int loanId);
        Task<IEnumerable<Repayment>> GetByCustomerIdAsync(int customerId);
        Task<Repayment> CreateAsync(Repayment repayment);
        Task<Repayment?> DeleteAsync(int id);

    }
}
