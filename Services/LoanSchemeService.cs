using Loan_Management_System.Models;
using Loan_Management_System.Repository;

namespace Loan_Management_System.Services
{
    public class LoanSchemeService : ILoanSchemeService
    {
        private readonly ILoanSchemeRepository _repository;

        public LoanSchemeService(ILoanSchemeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<LoanScheme>> GetAllSchemesAsync() =>
            await _repository.GetAllAsync();

        public async Task<LoanScheme?> GetSchemeByIdAsync(int id) =>
            await _repository.GetByIdAsync(id);

        public async Task<LoanScheme> CreateSchemeAsync(LoanScheme scheme) =>
            await _repository.CreateAsync(scheme);

        public async Task<LoanScheme?> UpdateSchemeAsync(LoanScheme scheme) =>
            await _repository.UpdateAsync(scheme);

        public async Task<LoanScheme?> DeleteSchemeAsync(int id) =>
            await _repository.DeleteAsync(id);
    }

}
