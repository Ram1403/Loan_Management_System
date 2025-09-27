using Loan_Management_System.Models;
using Loan_Management_System.Repository;

public interface IAdminService
{
    Task<LoanAdmin> CreateAdminAsync(LoanAdmin admin);
    Task<LoanAdmin?> UpdateAdminAsync(LoanAdmin admin);
    Task<LoanAdmin?> DeleteAdminAsync(int id);
    Task<LoanAdmin?> GetAdminByIdAsync(int id);
    Task<List<LoanAdmin>> GetAllAdminsAsync();
    Task<LoanScheme> CreateLoanSchemeAsync(int adminId, LoanScheme scheme);
    Task<LoanScheme?> UpdateLoanSchemeAsync(int schemeId, LoanScheme scheme);
    Task<bool> DeleteLoanSchemeAsync(int schemeId);
    //Task<LoanOfficer?> AssignOfficerAsync(int officerId);
    Task<LoanApplication?> AssignOfficerToApplicationAsync(int applicationId, int officerId);
    Task<LoanAdmin?> DeactivateAdminAsync(int id);
    Task<LoanAdmin?> ReactivateAdminAsync(int id);
    Task<List<string>> GetAuditLogAsync(int adminId);
}




//using Loan_Management_System.Models;

//namespace Loan_Management_System.Services
//{
//    public interface IAdminService
//    {
//        Task<LoanAdmin> Create(LoanAdmin loanAdmin);
//        Task<LoanAdmin> Update(LoanAdmin loanAdmin);
//        Task<LoanAdmin> Delete(int id);
//        Task<LoanAdmin> GetById(int id);

//        Task<List<LoanAdmin>> GetAll();
//    }
//}
