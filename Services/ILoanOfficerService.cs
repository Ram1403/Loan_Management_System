using Loan_Management_System.Models;

namespace Loan_Management_System.Services
{
    public interface ILoanOfficerService
    {
        Task<IEnumerable<LoanOfficer>> GetAllOfficersAsync();
        Task<LoanOfficer?> GetOfficerByIdAsync(int id);
        Task<LoanOfficer> CreateOfficerAsync(LoanOfficer officer);
        Task<LoanOfficer?> UpdateOfficerAsync(LoanOfficer officer);
        Task<LoanOfficer?> DeleteOfficerAsync(int id);
    }

}
