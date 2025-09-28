using Loan_Management_System.Models;
using Loan_Management_System.Repository;

namespace Loan_Management_System.Services
{
    public class LoanDocumentService : ILoanDocumentService
    {
        private readonly ILoanDocumentRepository _repo;

        public LoanDocumentService(ILoanDocumentRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<LoanDocument>> GetAllAsync() => _repo.GetAllAsync();
        public Task<IEnumerable<LoanDocument>> GetByApplicationAsync(int appId) => _repo.GetByApplicationAsync(appId);
        public Task<IEnumerable<LoanDocument>> GetByCustomerAsync(int customerId) => _repo.GetByCustomerAsync(customerId);
        public Task<IEnumerable<LoanDocument>> GetPendingAsync() => _repo.GetPendingAsync();
        public Task<LoanDocument?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<LoanDocument> AddAsync(LoanDocument doc) => _repo.AddAsync(doc);
        public Task<LoanDocument?> VerifyAsync(int id, Status status, string remarks, int officerId) => _repo.VerifyAsync(id, status, remarks, officerId);
        public Task<LoanDocument?> RejectAsync(int id, string remarks, int officerId) => _repo.RejectAsync(id, remarks, officerId);
    }
}
