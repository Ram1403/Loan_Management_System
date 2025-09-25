using Loan_Management_System.DTOs;
using Loan_Management_System.Models;
using Loan_Management_System.Repository;

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

    // ✅ New methods
    public async Task<bool> DeleteApplicationAsync(int id) =>
        await _repository.DeleteAsync(id);

    public async Task<LoanApplication?> UpdateApplicationAsync(LoanApplication application) =>
        await _repository.UpdateAsync(application);

    public async Task<IEnumerable<LoanApplication>> GetByStatusAsync(Status status) =>
        await _repository.GetByStatusAsync(status);

    public async Task<IEnumerable<LoanApplication>> GetByDateRangeAsync(DateTime start, DateTime end) =>
        await _repository.GetByDateRangeAsync(start, end);

    public async Task<IEnumerable<LoanApplication>> SearchAsync(string query) =>
        await _repository.SearchAsync(query);

    public async Task<IEnumerable<LoanApplication>> GetRecentByCustomerAsync(int customerId) =>
        await _repository.GetRecentByCustomerAsync(customerId);

    public async Task<IEnumerable<LoanApplication>> GetByCustomerAndStatusAsync(int customerId, Status status) =>
        await _repository.GetByCustomerAndStatusAsync(customerId, status);

    public async Task<ApplicationSummary> GetSummaryByCustomerAsync(int customerId) =>
        await _repository.GetSummaryByCustomerAsync(customerId);
}



//old version:
//using Loan_Management_System.Models;
//using Loan_Management_System.Repository;

//namespace Loan_Management_System.Services
//{
//    public class LoanApplicationService : ILoanApplicationService
//    {
//        private readonly ILoanApplicationRepository _repository;

//        public LoanApplicationService(ILoanApplicationRepository repository)
//        {
//            _repository = repository;
//        }

//        public async Task<IEnumerable<LoanApplication>> GetAllApplicationsAsync() =>
//            await _repository.GetAllAsync();

//        public async Task<LoanApplication?> GetApplicationByIdAsync(int id) =>
//            await _repository.GetByIdAsync(id);

//        public async Task<LoanApplication> CreateApplicationAsync(LoanApplication application) =>
//            await _repository.CreateAsync(application);

//        public async Task<LoanApplication?> UpdateApplicationStatusAsync(int id, Status newStatus, string? remarks) =>
//            await _repository.UpdateStatusAsync(id, newStatus, remarks);

//        public async Task<LoanApplication?> AssignLoanOfficerAsync(int id, int officerId) =>
//            await _repository.AssignOfficerAsync(id, officerId);
//    }

//}
