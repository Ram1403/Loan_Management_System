using System;
using Loan_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Loan_Management_System.Data;

namespace Loan_Management_System.Repository
{
    public class NpaRepository : INpaRepository
    {
        private readonly LoanDbContext _context;

        public NpaRepository(LoanDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Npa>> GetAllAsync() =>
            await _context.Npas
                .Include(n => n.Loan)
                .ThenInclude(l => l.LoanApplication)
                .ToListAsync();

        public async Task<IEnumerable<Npa>> GetByLoanIdAsync(int loanId) =>
            await _context.Npas
                .Include(n => n.Loan)
                .Where(n => n.LoanId == loanId)
                .ToListAsync();

        public async Task<Npa> FlagAsNpaAsync(Npa npa)
        {
            // ✅ Flag loan as NPA
            var loan = await _context.Loans.FindAsync(npa.LoanId);
            if (loan != null)
            {
                loan.LoanStatus = LoanStatus.NPA;
                loan.UpdatedAt = DateTime.UtcNow;
            }

            npa.NpaDate = DateTime.UtcNow;
            npa.CreatedAt = DateTime.UtcNow;

            await _context.Npas.AddAsync(npa);
            await _context.SaveChangesAsync();

            return npa;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var npa = await _context.Npas.FindAsync(id);
            if (npa == null) return false;

            _context.Npas.Remove(npa);

            // Reset loan status if this was the only NPA
            var loan = await _context.Loans.FindAsync(npa.LoanId);
            if (loan != null && loan.LoanStatus == LoanStatus.NPA)
            {
                // Admin may choose to revert status → here we set back to Active
                loan.LoanStatus = LoanStatus.Active;
                loan.UpdatedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
    

}
