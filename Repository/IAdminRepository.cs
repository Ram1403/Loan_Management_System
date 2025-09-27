using Loan_Management_System.Models;

namespace Loan_Management_System.Repository
{
    public interface IAdminRepository
    {
        Task<LoanAdmin> CreateAsync(LoanAdmin loanAdmin);
        Task<LoanAdmin?> UpdateAsync(LoanAdmin loanAdmin);
        Task<LoanAdmin?> DeleteAsync(int id);
        Task<LoanAdmin?> GetByIdAsync(int id);
        Task<List<LoanAdmin>> GetAllAsync();

        // LoanScheme management
        Task<LoanScheme> CreateLoanSchemeAsync(int adminId, LoanScheme scheme);
        Task<LoanScheme?> UpdateLoanSchemeAsync(int schemeId, LoanScheme scheme);
        Task<bool> DeleteLoanSchemeAsync(int schemeId);

        // Officer assignment (for simplicity: just returns officer)
        //Task<LoanOfficer?> AssignOfficerAsync(int officerId);
        Task<LoanApplication?> AssignOfficerToApplicationAsync(int applicationId, int officerId);

        // Admin status
        Task<LoanAdmin?> DeactivateAsync(int adminId);
        Task<LoanAdmin?> ReactivateAsync(int adminId);

        // Audit log
        Task<List<string>> GetAuditLogAsync(int adminId);
    }
}



//using Loan_Management_System.Models;
//namespace Loan_Management_System.Repository
//{
//    public interface IAdminRepository
//    {
//        Task<LoanAdmin>Create(LoanAdmin loanAdmin);
//        Task<LoanAdmin> Update(LoanAdmin loanAdmin);
//        Task<LoanAdmin> Delete(int id);
//        Task<LoanAdmin> GetById(int id);

//        Task<List<LoanAdmin>>GetAll();


//    }
//}
