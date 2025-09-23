using Loan_Management_System.Models;

namespace Loan_Management_System.Repository
{
    public interface ILoanApplicationRepository
    {
        Task<IEnumerable<LoanApplication>> GetAllAsync();
        Task<LoanApplication?> GetByIdAsync(int id);
        Task<LoanApplication> CreateAsync(LoanApplication application);
        Task<LoanApplication?> UpdateStatusAsync(int id, Status newStatus, string? remarks);
        Task<LoanApplication?> AssignOfficerAsync(int id, int officerId);
    }
}
