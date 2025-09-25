using Loan_Management_System.Models;
using Loan_Management_System.Repository;

public class RepaymentService : IRepaymentService
{
    private readonly IRepaymentRepository _repository;

    public RepaymentService(IRepaymentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Repayment>> GetAllAsync() =>
        await _repository.GetAllAsync();

    public async Task<Repayment?> GetByIdAsync(int id) =>
        await _repository.GetByIdAsync(id);

    public async Task<Repayment?> DeleteAsync(int id) =>
    await _repository.DeleteAsync(id);

    public async Task<IEnumerable<Repayment>> GetByLoanIdAsync(int loanId) =>
        await _repository.GetByLoanIdAsync(loanId);

    public async Task<IEnumerable<Repayment>> GetByCustomerIdAsync(int customerId) =>
        await _repository.GetByCustomerIdAsync(customerId);

    public async Task<Repayment> CreateAsync(Repayment repayment) =>
        await _repository.CreateAsync(repayment);
}
