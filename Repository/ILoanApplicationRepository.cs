using Loan_Management_System.DTOs;
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

        // ✅ New additions
        Task<bool> DeleteAsync(int id);
        Task<LoanApplication?> UpdateAsync(LoanApplication application);
        Task<IEnumerable<LoanApplication>> GetByStatusAsync(Status status);
        Task<IEnumerable<LoanApplication>> GetByDateRangeAsync(DateTime start, DateTime end);
        Task<IEnumerable<LoanApplication>> SearchAsync(string query);
        Task<IEnumerable<LoanApplication>> GetByCustomerAsync(int customerId);

        Task<IEnumerable<LoanApplication>> GetRecentByCustomerAsync(int customerId);
        Task<IEnumerable<LoanApplication>> GetByCustomerAndStatusAsync(int customerId, Status status);
        Task<ApplicationSummary> GetSummaryByCustomerAsync(int customerId);
    }
}














//namespace Loan_Management_System.Repository
//{
//    public interface ILoanApplicationRepository
//    {
//        Task<IEnumerable<LoanApplication>> GetAllAsync();
//        Task<LoanApplication?> GetByIdAsync(int id);
//        Task<LoanApplication> CreateAsync(LoanApplication application);
//        Task<LoanApplication?> UpdateStatusAsync(int id, Status newStatus, string? remarks);
//        Task<LoanApplication?> AssignOfficerAsync(int id, int officerId);
//    }
//}
