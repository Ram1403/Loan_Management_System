using Loan_Management_System.Models;
using Loan_Management_System.Repository;

namespace Loan_Management_System.Services
{
    public class LoanOfficerService : ILoanOfficerService
    {
        private readonly ILoanOfficerRepository _repository;

        public LoanOfficerService(ILoanOfficerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<LoanOfficer>> GetAllOfficersAsync() =>
            await _repository.GetAllAsync();

        public async Task<LoanOfficer?> GetOfficerByIdAsync(int id) =>
            await _repository.GetByIdAsync(id);

        public async Task<LoanOfficer> CreateOfficerAsync(LoanOfficer officer) =>
            await _repository.CreateAsync(officer);

        public async Task<LoanOfficer?> UpdateOfficerAsync(LoanOfficer officer) =>
            await _repository.UpdateAsync(officer);

        public async Task<LoanOfficer?> DeleteOfficerAsync(int id) =>
            await _repository.DeleteAsync(id);
    }

}
