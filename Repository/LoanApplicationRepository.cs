using System;
using Loan_Management_System.Data;
using Loan_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Loan_Management_System.Repository
{
    public class LoanApplicationRepository : ILoanApplicationRepository
    {
        private readonly LoanDbContext _context;

        public LoanApplicationRepository(LoanDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LoanApplication>> GetAllAsync() {
            var application =  await _context.LoanApplications
                .Include(a => a.Customer)
                .Include(a => a.LoanScheme)
                .Include(a => a.LoanOfficer)
                .ToListAsync();
                return application;
        }

        public async Task<LoanApplication?> GetByIdAsync(int id)
        {
            var application= await _context.LoanApplications
                .Include(a => a.Customer)
                .Include(a => a.LoanScheme)
                .Include(a => a.LoanOfficer)
                .FirstOrDefaultAsync(a => a.ApplicationId == id);
            return application;
        }
            

        public async Task<LoanApplication> CreateAsync(LoanApplication application)
        {
            await _context.LoanApplications.AddAsync(application);
            await _context.SaveChangesAsync();
            return application;
        }

        public async Task<LoanApplication?> UpdateStatusAsync(int id, Status newStatus, string? remarks)
        {
            var application = await _context.LoanApplications.FindAsync(id);
            if (application == null) return null;

            application.Status = newStatus;
            application.Remarks = remarks;
            application.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return application;
        }

        public async Task<LoanApplication?> AssignOfficerAsync(int id, int officerId)
        {
            var application = await _context.LoanApplications.FindAsync(id);
            if (application == null) return null;

            application.OfficerId = officerId;
            application.OfficerAssignedDate = DateTime.UtcNow;
            application.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return application;
        }
    }

}
