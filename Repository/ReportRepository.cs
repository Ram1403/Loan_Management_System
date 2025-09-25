using System;
using Loan_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Loan_Management_System.Data;

namespace Loan_Management_System.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly LoanDbContext _context;

        public ReportRepository(LoanDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Report>> GetAllAsync() =>
            await _context.Reports.Include(r => r.LoanAdmin).ToListAsync();

        public async Task<Report?> GetByIdAsync(int id) =>
            await _context.Reports.Include(r => r.LoanAdmin)
                                  .FirstOrDefaultAsync(r => r.ReportId == id);

        public async Task<IEnumerable<Report>> GetByAdminAsync(int adminId) =>
            await _context.Reports.Where(r => r.GeneratedBy == adminId).ToListAsync();

        public async Task<Report> CreateAsync(Report report)
        {
            report.GeneratedDate = DateTime.UtcNow;
            report.CreatedAt = DateTime.UtcNow;

            await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null) return false;

            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
