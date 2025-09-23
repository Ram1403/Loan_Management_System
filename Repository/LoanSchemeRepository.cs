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

        public async Task<IEnumerable<LoanScheme>> GetAllAsync() =>
            await _context.LoanSchemes.Include(s => s.LoanAdmin).ToListAsync();

        public async Task<LoanScheme?> GetByIdAsync(int id) =>
            await _context.LoanSchemes.Include(s => s.LoanAdmin)
                                      .FirstOrDefaultAsync(s => s.SchemeId == id);

        public async Task<LoanScheme> CreateAsync(LoanScheme scheme)
        {
            await _context.LoanSchemes.AddAsync(scheme);
            await _context.SaveChangesAsync();
            return scheme;
        }

        public async Task<LoanScheme?> UpdateAsync(LoanScheme scheme)
        {
            var existing = await _context.LoanSchemes.FindAsync(scheme.SchemeId);
            if (existing == null) return null;

            _context.Entry(existing).State = EntityState.Detached;
            _context.LoanSchemes.Update(scheme);
            await _context.SaveChangesAsync();
            return scheme;
        }

        public async Task<LoanScheme?> DeleteAsync(int id)
        {
            var scheme = await GetByIdAsync(id);
            if (scheme == null) return null;

            _context.LoanSchemes.Remove(scheme);
            await _context.SaveChangesAsync();
            return scheme;
        }

        public async Task<bool> ExistsAsync(int id) =>
            await _context.LoanSchemes.AnyAsync(s => s.SchemeId == id);
    }

}
