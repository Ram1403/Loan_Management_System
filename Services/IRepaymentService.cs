using Loan_Management_System.Models;

public interface IRepaymentService
{
    Task<IEnumerable<Repayment>> GetAllAsync();
    Task<Repayment?> GetByIdAsync(int id);
    Task<IEnumerable<Repayment>> GetByLoanIdAsync(int loanId);
    Task<IEnumerable<Repayment>> GetByCustomerIdAsync(int customerId);
    Task<Repayment> CreateAsync(Repayment repayment);
    Task<Repayment?> DeleteAsync(int id);

}
