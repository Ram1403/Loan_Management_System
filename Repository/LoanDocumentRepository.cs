using System;
using Loan_Management_System.Models;
using Loan_Management_System.Data;
using Microsoft.EntityFrameworkCore;


namespace Loan_Management_System.Repository
{
    public class LoanDocumentRepository: ILoanDocumentRepository
    {
        private readonly LoanDbContext _context;

        public LoanDocumentRepository(LoanDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LoanDocument>> GetAllAsync() =>
            await _context.LoanDocuments.ToListAsync();

        public async Task<IEnumerable<LoanDocument>> GetByApplicationAsync(int appId) =>
            await _context.LoanDocuments.Where(d => d.ApplicationId == appId).ToListAsync();

        public async Task<IEnumerable<LoanDocument>> GetByCustomerAsync(int customerId) =>
            await _context.LoanDocuments
                .Where(d => d.LoanApplication.CustomerId == customerId)
                .ToListAsync();

        public async Task<IEnumerable<LoanDocument>> GetPendingAsync() =>
            await _context.LoanDocuments.Where(d => d.VerificationStatus == Status.Pending).ToListAsync();

        public async Task<LoanDocument?> GetByIdAsync(int id) =>
            await _context.LoanDocuments.FindAsync(id);

        public async Task<LoanDocument> AddAsync(LoanDocument doc)
        {
            _context.LoanDocuments.Add(doc);
            await _context.SaveChangesAsync();
            return doc;
        }

        public async Task<LoanDocument?> VerifyAsync(int id, Status status, string remarks, int officerId)
        {
            var doc = await _context.LoanDocuments.FindAsync(id);
            if (doc == null) return null;

            doc.VerificationStatus = status;
            doc.VerificationRemarks = remarks;
            doc.VerifiedBy = officerId;
            doc.VerificationDate = DateTime.UtcNow;
            doc.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return doc;
        }

        public async Task<LoanDocument?> RejectAsync(int id, string remarks, int officerId)
        {
            var doc = await _context.LoanDocuments.FindAsync(id);
            if (doc == null) return null;

            doc.VerificationStatus = Status.Rejected;
            doc.VerificationRemarks = remarks;
            doc.VerifiedBy = officerId;
            doc.VerificationDate = DateTime.UtcNow;
            doc.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return doc;
        }
    }
}

