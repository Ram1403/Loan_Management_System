using System;
using Loan_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Loan_Management_System.Data;

namespace Loan_Management_System.Repository
{
    public class LoanOfficerRepository : ILoanOfficerRepository
    {
        private readonly LoanDbContext _context;

        public LoanOfficerRepository(LoanDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LoanOfficer>> GetAllOfficersAsync() =>
            await _context.LoanOfficers.Include(o => o.User).ToListAsync();

        public async Task<LoanOfficer?> GetOfficerByIdAsync(int id) =>
            await _context.LoanOfficers.Include(o => o.User)
                                       .FirstOrDefaultAsync(o => o.OfficerId == id);

        public async Task<LoanOfficer> CreateOfficerAsync(LoanOfficer officer)
        {
            await _context.LoanOfficers.AddAsync(officer);
            await _context.SaveChangesAsync();
            return officer;
        }

        public async Task<LoanOfficer?> UpdateOfficerAsync(LoanOfficer officer)
        {
            var existing = await _context.LoanOfficers.FindAsync(officer.OfficerId);
            if (existing == null) return null;

            _context.Entry(existing).State = EntityState.Detached;
            _context.LoanOfficers.Update(officer);
            await _context.SaveChangesAsync();
            return officer;
        }

        public async Task<LoanOfficer?> DeleteOfficerAsync(int id)
        {
            var officer = await GetOfficerByIdAsync(id);
            if (officer == null) return null;

            _context.LoanOfficers.Remove(officer);
            await _context.SaveChangesAsync();
            return officer;
        }

        public async Task<LoanOfficer?> UpdateWorkloadAsync(int officerId, int count)
        {
            var officer = await _context.LoanOfficers.FindAsync(officerId);
            if (officer == null) return null;

            officer.CurrentWorkload = count;
            officer.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return officer;
        }

        public async Task<LoanApplication?> AssignLoanApplicationAsync(int officerId, int applicationId)
        {
            var officer = await _context.LoanOfficers.FindAsync(officerId);
            var application = await _context.LoanApplications.FindAsync(applicationId);

            if (officer == null || application == null) return null;

            application.OfficerId = officerId;
            application.UpdatedAt = DateTime.UtcNow;

            officer.CurrentWorkload += 1;
            officer.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return application;
        }

        public async Task<LoanDocument?> VerifyDocumentAsync(int officerId, int documentId, Status status, string remarks)
        {
            var officer = await _context.LoanOfficers.FindAsync(officerId);
            var document = await _context.LoanDocuments.FindAsync(documentId);

            if (officer == null || document == null) return null;

            document.VerifiedBy = officerId;
            document.VerificationStatus = status;         // Approved / Rejected
            document.VerificationRemarks = remarks;
            document.VerificationDate = DateTime.UtcNow;
            document.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return document;
        }



        public async Task<CustomerQuery?> RespondToQueryAsync(int officerId, int queryId, string response)
        {
            var query = await _context.CustomerQueries.FindAsync(queryId);
            if (query == null) return null;

            query.OfficerId = officerId;
            query.OfficerResponse = response;
            query.QueryStatus = QueryStatus.Resolved;   // mark as resolved after response
            query.ResolvedAt = DateTime.UtcNow;
            query.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return query;
        }


        public async Task<IEnumerable<LoanApplication>> GetAssignedApplicationsAsync(int officerId) =>
            await _context.LoanApplications.Where(a => a.OfficerId == officerId).ToListAsync();

        public async Task<IEnumerable<LoanDocument>> GetVerifiedDocumentsAsync(int officerId) =>
     await _context.LoanDocuments
         .Where(d => d.VerifiedBy == officerId && d.VerificationStatus == Status.Approved)
         .ToListAsync();


        public async Task<IEnumerable<CustomerQuery>> GetHandledQueriesAsync(int officerId) =>
     await _context.CustomerQueries
         .Where(q => q.OfficerId == officerId && q.QueryStatus == QueryStatus.Resolved)
         .ToListAsync();


        public async Task<LoanOfficer?> UpdatePerformanceAsync(int officerId, decimal rating)
        {
            var officer = await _context.LoanOfficers.FindAsync(officerId);
            if (officer == null) return null;

            officer.PerformanceRating = rating;
            officer.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return officer;
        }

        public async Task<LoanOfficer?> DeactivateOfficerAsync(int officerId)
        {
            var officer = await _context.LoanOfficers.FindAsync(officerId);
            if (officer == null) return null;

            officer.IsActive = false;
            officer.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return officer;
        }

        public async Task<LoanOfficer?> ReactivateOfficerAsync(int officerId)
        {
            var officer = await _context.LoanOfficers.FindAsync(officerId);
            if (officer == null) return null;

            officer.IsActive = true;
            officer.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return officer;
        }

        public async Task<List<string>> GetAuditLogAsync(int officerId)
        {
            // ⚡ Example: fetch from real audit table later
            return new List<string>
            {
                $"Officer {officerId} verified a document at {DateTime.UtcNow}",
                $"Officer {officerId} responded to query at {DateTime.UtcNow}"
            };
        }
    }
}
