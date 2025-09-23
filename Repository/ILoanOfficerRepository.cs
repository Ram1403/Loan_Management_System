using Loan_Management_System.Models;

namespace Loan_Management_System.Repository
{
    public interface ILoanOfficerRepository
    {
        Task<IEnumerable<LoanOfficer>> GetAllAsync();
        Task<LoanOfficer?> GetByIdAsync(int id);
        Task<LoanOfficer> CreateAsync(LoanOfficer officer);
        Task<LoanOfficer?> UpdateAsync(LoanOfficer officer);
        Task<LoanOfficer?> DeleteAsync(int id);
       
    }
}
