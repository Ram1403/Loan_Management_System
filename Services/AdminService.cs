using Loan_Management_System.Models;
using Loan_Management_System.Repository;
using Microsoft.EntityFrameworkCore;
namespace Loan_Management_System.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _repository;
        public AdminService(IAdminRepository repository) { _repository = repository; }

        public Task<LoanAdmin> CreateAdminAsync(LoanAdmin admin) => _repository.CreateAsync(admin);
        public Task<LoanAdmin?> UpdateAdminAsync(LoanAdmin admin) => _repository.UpdateAsync(admin);
        public Task<LoanAdmin?> DeleteAdminAsync(int id) => _repository.DeleteAsync(id);
        public Task<LoanAdmin?> GetAdminByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task<List<LoanAdmin>> GetAllAdminsAsync() => _repository.GetAllAsync();
        public Task<LoanScheme> CreateLoanSchemeAsync(int adminId, LoanScheme scheme) => _repository.CreateLoanSchemeAsync(adminId, scheme);
        public Task<LoanScheme?> UpdateLoanSchemeAsync(int schemeId, LoanScheme scheme) => _repository.UpdateLoanSchemeAsync(schemeId, scheme);
        public Task<bool> DeleteLoanSchemeAsync(int schemeId) => _repository.DeleteLoanSchemeAsync(schemeId);
        //public Task<LoanOfficer?> AssignOfficerAsync(int officerId) => _repository.AssignOfficerAsync(officerId);
        public Task<LoanApplication?> AssignOfficerToApplicationAsync(int applicationId, int officerId) =>
     _repository.AssignOfficerToApplicationAsync(applicationId, officerId);
        public Task<LoanAdmin?> DeactivateAdminAsync(int id) => _repository.DeactivateAsync(id);
        public Task<LoanAdmin?> ReactivateAdminAsync(int id) => _repository.ReactivateAsync(id);
        public Task<List<string>> GetAuditLogAsync(int adminId) => _repository.GetAuditLogAsync(adminId);
    }
}
