using Loan_Management_System.Models;
using Loan_Management_System.Repository;

namespace Loan_Management_System.Services
{
    public class LoanApplicationService : ILoanApplicationService
    {
        private readonly ILoanApplicationRepository _repository;

        public LoanApplicationService(ILoanApplicationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<LoanApplication>> GetAllApplicationsAsync() =>
            await _repository.GetAllAsync();

        public async Task<LoanApplication?> GetApplicationByIdAsync(int id) =>
            await _repository.GetByIdAsync(id);

        public async Task<LoanApplication> CreateApplicationAsync(LoanApplication application) =>
            await _repository.CreateAsync(application);

        public async Task<LoanApplication?> UpdateApplicationStatusAsync(int id, Status newStatus, string? remarks) =>
            await _repository.UpdateStatusAsync(id, newStatus, remarks);

        public async Task<LoanApplication?> AssignLoanOfficerAsync(int id, int officerId) =>
            await _repository.AssignOfficerAsync(id, officerId);
    }

}
