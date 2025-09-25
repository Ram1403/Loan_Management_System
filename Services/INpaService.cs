using Loan_Management_System.Models;

public interface INpaService
{
    Task<IEnumerable<Npa>> GetAllAsync();
    Task<IEnumerable<Npa>> GetByLoanIdAsync(int loanId);
    Task<Npa> FlagAsNpaAsync(Npa npa);
    Task<bool> DeleteAsync(int id);   // ✅ new

}
