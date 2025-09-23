using Loan_Management_System.Models;

namespace Loan_Management_System.Services
{
    public interface ILoanApplicationService
    {
        Task<IEnumerable<LoanApplication>> GetAllApplicationsAsync();
        Task<LoanApplication?> GetApplicationByIdAsync(int id);
        Task<LoanApplication> CreateApplicationAsync(LoanApplication application);
        Task<LoanApplication?> UpdateApplicationStatusAsync(int id, Status newStatus, string? remarks);
        Task<LoanApplication?> AssignLoanOfficerAsync(int id, int officerId);
    }

}
