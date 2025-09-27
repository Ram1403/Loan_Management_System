using Loan_Management_System.Models;
using Loan_Management_System.Repository;

public class LoanSchemeService : ILoanSchemeService
{
    private readonly ILoanSchemeRepository _repository;

    public LoanSchemeService(ILoanSchemeRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<LoanScheme>> GetAllSchemesAsync() =>
        _repository.GetAllSchemesAsync();

    public Task<LoanScheme?> GetSchemeByIdAsync(int id) =>
        _repository.GetSchemeByIdAsync(id);

    public Task<LoanScheme> CreateSchemeAsync(LoanScheme scheme) =>
        _repository.CreateSchemeAsync(scheme);

    public Task<LoanScheme?> UpdateSchemeAsync(LoanScheme scheme) =>
        _repository.UpdateSchemeAsync(scheme);

    public Task<bool> DeleteSchemeAsync(int id) =>
        _repository.DeleteSchemeAsync(id);
}
