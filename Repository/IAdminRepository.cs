using Loan_Management_System.Models;
namespace Loan_Management_System.Repository
{
    public interface IAdminRepository
    {
        Task<LoanAdmin>Create(LoanAdmin loanAdmin);
        Task<LoanAdmin> Update(LoanAdmin loanAdmin);
        Task<LoanAdmin> Delete(int id);
        Task<LoanAdmin> GetById(int id);

        Task<List<LoanAdmin>>GetAll();

        
    }
}
