using System;
using Loan_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Loan_Management_System.Data;

namespace Loan_Management_System.Repository
{
    public class LoanSchemeRepository : ILoanSchemeRepository
    {
        private readonly LoanDbContext _context;

        public LoanSchemeRepository(LoanDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LoanScheme>> GetAllSchemesAsync() =>
            await _context.LoanSchemes.Where(s => s.IsActive).ToListAsync();

        public async Task<LoanScheme?> GetSchemeByIdAsync(int id) =>
            await _context.LoanSchemes.FirstOrDefaultAsync(s => s.SchemeId == id && s.IsActive);

        public async Task<LoanScheme> CreateSchemeAsync(LoanScheme scheme)
        {
            scheme.CreatedAt = DateTime.UtcNow;
            scheme.IsActive = true;
            await _context.LoanSchemes.AddAsync(scheme);
            await _context.SaveChangesAsync();
            return scheme;
        }

        public async Task<LoanScheme?> UpdateSchemeAsync(LoanScheme scheme)
        {
            var existing = await _context.LoanSchemes.FindAsync(scheme.SchemeId);
            if (existing == null) return null;

            existing.SchemeName = scheme.SchemeName;
            existing.InterestRate = scheme.InterestRate;
            existing.MaxAmount = scheme.MaxAmount;
            existing.MinAmount = scheme.MinAmount;
            existing.TenureMonths = scheme.TenureMonths;
            existing.EligibilityCriteria = scheme.EligibilityCriteria;
            existing.IsActive = scheme.IsActive;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteSchemeAsync(int id)
        {
            var scheme = await _context.LoanSchemes.FindAsync(id);
            if (scheme == null) return false;

            _context.LoanSchemes.Remove(scheme);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
