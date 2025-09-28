using Loan_Management_System.Models;

namespace Loan_Management_System.Repository
{
    public interface ILoanDocumentRepository
    {
        Task<IEnumerable<LoanDocument>> GetAllAsync();
        Task<IEnumerable<LoanDocument>> GetByApplicationAsync(int appId);
        Task<IEnumerable<LoanDocument>> GetByCustomerAsync(int customerId);
        Task<IEnumerable<LoanDocument>> GetPendingAsync();
        Task<LoanDocument?> GetByIdAsync(int id);
        Task<LoanDocument> AddAsync(LoanDocument doc);
        Task<LoanDocument?> VerifyAsync(int id, Status status, string remarks, int officerId);
        Task<LoanDocument?> RejectAsync(int id, string remarks, int officerId);
    }

}
