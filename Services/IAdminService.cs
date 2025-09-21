using Loan_Management_System.Models;

namespace Loan_Management_System.Services
{
    public interface IAdminService
    {
        Task<LoanAdmin> Create(LoanAdmin loanAdmin);
        Task<LoanAdmin> Update(LoanAdmin loanAdmin);
        Task<LoanAdmin> Delete(int id);
        Task<LoanAdmin> GetById(int id);

        Task<List<LoanAdmin>> GetAll();
    }
}
