using Loan_Management_System.Models;
using Loan_Management_System.Repository;
using Microsoft.EntityFrameworkCore;
namespace Loan_Management_System.Services
{
    public class AdminService: IAdminService
    {
        private readonly IAdminRepository _adminService;
        public AdminService(IAdminRepository adminService)
        {
            _adminService = adminService;
        }
        public async Task<LoanAdmin> Create(LoanAdmin loanAdmin)
        {
            return await _adminService.Create(loanAdmin);
        }
        public async Task<LoanAdmin> Update(LoanAdmin loanAdmin)
        {
            return await _adminService.Update(loanAdmin);
        }
        public async Task<LoanAdmin> Delete(int id)
        {
            return await _adminService.Delete(id);
        }
        public async Task<LoanAdmin> GetById(int id)
        {
            return await _adminService.GetById(id);
        }
        public async Task<List<LoanAdmin>> GetAll()
        {
            return await _adminService.GetAll();
        }
    }
}
