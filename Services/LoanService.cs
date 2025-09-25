using Loan_Management_System.Models;
using Loan_Management_System.Repository;

namespace Loan_Management_System.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _repository;

        public LoanService(ILoanRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Loan>> GetAllLoansAsync() =>
            await _repository.GetAllAsync();

        public async Task<Loan?> GetLoanByIdAsync(int id) =>
            await _repository.GetByIdAsync(id);

        public async Task<Loan> CreateLoanAsync(Loan loan) =>
            await _repository.CreateAsync(loan);

        public async Task<Loan?> UpdateLoanAsync(Loan loan) =>
            await _repository.UpdateAsync(loan);

        public async Task<Loan?> TrackEmiAsync(int loanId, int emiPaid) =>
            await _repository.TrackEmiAsync(loanId, emiPaid);
        public async Task<Loan?> DeleteLoanAsync(int id) =>
            await _repository.DeleteAsync(id);

    }

}
