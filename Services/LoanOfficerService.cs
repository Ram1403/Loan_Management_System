using Loan_Management_System.Models;
using Loan_Management_System.Repository;

public class LoanOfficerService : ILoanOfficerService
{
    private readonly ILoanOfficerRepository _repository;
    public LoanOfficerService(ILoanOfficerRepository repository) { _repository = repository; }

    public Task<IEnumerable<LoanOfficer>> GetAllOfficersAsync() => _repository.GetAllOfficersAsync();
    public Task<LoanOfficer?> GetOfficerByIdAsync(int id) => _repository.GetOfficerByIdAsync(id);
    public Task<LoanOfficer> CreateOfficerAsync(LoanOfficer officer) => _repository.CreateOfficerAsync(officer);
    public Task<LoanOfficer?> UpdateOfficerAsync(LoanOfficer officer) => _repository.UpdateOfficerAsync(officer);
    public Task<LoanOfficer?> DeleteOfficerAsync(int id) => _repository.DeleteOfficerAsync(id);

    public Task<LoanOfficer?> UpdateWorkloadAsync(int officerId, int count) => _repository.UpdateWorkloadAsync(officerId, count);
    public Task<LoanApplication?> AssignLoanApplicationAsync(int officerId, int applicationId) => _repository.AssignLoanApplicationAsync(officerId, applicationId);
    public Task<LoanDocument?> VerifyDocumentAsync(int officerId, int documentId, Status status, string remarks) =>
        _repository.VerifyDocumentAsync(officerId, documentId, status, remarks);

    public Task<CustomerQuery?> RespondToQueryAsync(int officerId, int queryId, string response) => _repository.RespondToQueryAsync(officerId, queryId, response);
    public Task<IEnumerable<LoanApplication>> GetAssignedApplicationsAsync(int officerId) => _repository.GetAssignedApplicationsAsync(officerId);
    public Task<IEnumerable<LoanDocument>> GetVerifiedDocumentsAsync(int officerId) => _repository.GetVerifiedDocumentsAsync(officerId);
    public Task<IEnumerable<CustomerQuery>> GetHandledQueriesAsync(int officerId) => _repository.GetHandledQueriesAsync(officerId);
    public Task<LoanOfficer?> UpdatePerformanceAsync(int officerId, decimal rating) => _repository.UpdatePerformanceAsync(officerId, rating);
    public Task<LoanOfficer?> DeactivateOfficerAsync(int officerId) => _repository.DeactivateOfficerAsync(officerId);
    public Task<LoanOfficer?> ReactivateOfficerAsync(int officerId) => _repository.ReactivateOfficerAsync(officerId);
    public Task<List<string>> GetAuditLogAsync(int officerId) => _repository.GetAuditLogAsync(officerId);
}
