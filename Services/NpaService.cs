using Loan_Management_System.Models;
using Loan_Management_System.Repository;

public class NpaService : INpaService
{
    private readonly INpaRepository _repository;

    public NpaService(INpaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Npa>> GetAllAsync() =>
        await _repository.GetAllAsync();

    public async Task<IEnumerable<Npa>> GetByLoanIdAsync(int loanId) =>
        await _repository.GetByLoanIdAsync(loanId);

    public async Task<Npa> FlagAsNpaAsync(Npa npa) =>
        await _repository.FlagAsNpaAsync(npa);

    public async Task<bool> DeleteAsync(int id) =>
    await _repository.DeleteAsync(id);

}
