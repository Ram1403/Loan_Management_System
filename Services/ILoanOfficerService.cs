using Loan_Management_System.Models;

public interface ILoanOfficerService
{
    Task<IEnumerable<LoanOfficer>> GetAllOfficersAsync();
    Task<LoanOfficer?> GetOfficerByIdAsync(int id);
    Task<LoanOfficer> CreateOfficerAsync(LoanOfficer officer);
    Task<LoanOfficer?> UpdateOfficerAsync(LoanOfficer officer);
    Task<LoanOfficer?> DeleteOfficerAsync(int id);

    Task<LoanOfficer?> UpdateWorkloadAsync(int officerId, int count);
    Task<LoanApplication?> AssignLoanApplicationAsync(int officerId, int applicationId);
    Task<LoanDocument?> VerifyDocumentAsync(int officerId, int documentId, Status status, string remarks);

    Task<CustomerQuery?> RespondToQueryAsync(int officerId, int queryId, string response);
    Task<IEnumerable<LoanApplication>> GetAssignedApplicationsAsync(int officerId);
    Task<IEnumerable<LoanDocument>> GetVerifiedDocumentsAsync(int officerId);
    Task<IEnumerable<CustomerQuery>> GetHandledQueriesAsync(int officerId);
    Task<LoanOfficer?> UpdatePerformanceAsync(int officerId, decimal rating);
    Task<LoanOfficer?> DeactivateOfficerAsync(int officerId);
    Task<LoanOfficer?> ReactivateOfficerAsync(int officerId);
    Task<List<string>> GetAuditLogAsync(int officerId);
}
