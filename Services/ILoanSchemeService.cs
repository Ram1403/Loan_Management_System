using Loan_Management_System.Models;

public interface ILoanSchemeService
{
    Task<IEnumerable<LoanScheme>> GetAllSchemesAsync();
    Task<LoanScheme?> GetSchemeByIdAsync(int id);
    Task<LoanScheme> CreateSchemeAsync(LoanScheme scheme);
    Task<LoanScheme?> UpdateSchemeAsync(LoanScheme scheme);
    Task<bool> DeleteSchemeAsync(int id);
}
