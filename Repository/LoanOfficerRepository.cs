using System;
using Loan_Management_System.Data;
using Loan_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Loan_Management_System.Repository
{
    public class LoanOfficerRepository : ILoanOfficerRepository
    {
        private readonly LoanDbContext _context;

        public LoanOfficerRepository(LoanDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LoanOfficer>> GetAllAsync() =>
            await _context.LoanOfficers.Include(o => o.User).ToListAsync();

        public async Task<LoanOfficer?> GetByIdAsync(int id) =>
            await _context.LoanOfficers.Include(o => o.User)
                                       .FirstOrDefaultAsync(o => o.OfficerId == id);

        public async Task<LoanOfficer> CreateAsync(LoanOfficer officer)
        {
            await _context.LoanOfficers.AddAsync(officer);
            await _context.SaveChangesAsync();
            return officer;
        }

        public async Task<LoanOfficer?> UpdateAsync(LoanOfficer officer)
        {
            var existing = await _context.LoanOfficers.FindAsync(officer.OfficerId);
            if (existing == null) return null;

            _context.Entry(existing).State = EntityState.Detached;
            _context.LoanOfficers.Update(officer);
            await _context.SaveChangesAsync();
            return officer;
        }

        public async Task<LoanOfficer?> DeleteAsync(int id)
        {
            var officer = await GetByIdAsync(id);
            if (officer == null) return null;

            _context.LoanOfficers.Remove(officer);
            await _context.SaveChangesAsync();
            return officer;
        }

    }

}
