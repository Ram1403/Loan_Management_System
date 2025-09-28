using Loan_Management_System.DTOs;
using Loan_Management_System.Models;

public interface ILoanApplicationService
{
    Task<IEnumerable<LoanApplication>> GetAllApplicationsAsync();
    Task<LoanApplication?> GetApplicationByIdAsync(int id);
    Task<LoanApplication> CreateApplicationAsync(LoanApplication application);
    Task<LoanApplication?> UpdateApplicationStatusAsync(int id, Status newStatus, string? remarks);
    Task<LoanApplication?> AssignLoanOfficerAsync(int id, int officerId);
    Task<IEnumerable<LoanApplication>> GetByCustomerAsync(int customerId);


    // ✅ New methods
    Task<bool> DeleteApplicationAsync(int id);
    Task<LoanApplication?> UpdateApplicationAsync(LoanApplication application);
    Task<IEnumerable<LoanApplication>> GetByStatusAsync(Status status);
    Task<IEnumerable<LoanApplication>> GetByDateRangeAsync(DateTime start, DateTime end);
    Task<IEnumerable<LoanApplication>> SearchAsync(string query);
    Task<IEnumerable<LoanApplication>> GetRecentByCustomerAsync(int customerId);
    Task<IEnumerable<LoanApplication>> GetByCustomerAndStatusAsync(int customerId, Status status);
    Task<ApplicationSummary> GetSummaryByCustomerAsync(int customerId);
}











//old version:
//using Loan_Management_System.Models;

//namespace Loan_Management_System.Services
//{
//    public interface ILoanApplicationService
//    {
//        Task<IEnumerable<LoanApplication>> GetAllApplicationsAsync();
//        Task<LoanApplication?> GetApplicationByIdAsync(int id);
//        Task<LoanApplication> CreateApplicationAsync(LoanApplication application);
//        Task<LoanApplication?> UpdateApplicationStatusAsync(int id, Status newStatus, string? remarks);
//        Task<LoanApplication?> AssignLoanOfficerAsync(int id, int officerId);
//    }

//}
