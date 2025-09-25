using System;
using Loan_Management_System.Data;
using Loan_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Loan_Management_System.Repository
{
    public class LoanRepository : ILoanRepository
    {
        private readonly LoanDbContext _context;

        public LoanRepository(LoanDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Loan>> GetAllAsync() =>
            await _context.Loans
                .Include(l => l.LoanApplication)
                .Include(l => l.Npa)
                .ToListAsync();

        public async Task<Loan?> GetByIdAsync(int id) =>
            await _context.Loans
                .Include(l => l.LoanApplication)
                .Include(l => l.Npa)
                .FirstOrDefaultAsync(l => l.LoanId == id);

        public async Task<Loan> CreateAsync(Loan loan)
        {
            await _context.Loans.AddAsync(loan);
            await _context.SaveChangesAsync();
            return loan;
        }

        public async Task<Loan?> UpdateAsync(Loan loan)
        {
            var existing = await _context.Loans.FindAsync(loan.LoanId);
            if (existing == null) return null;

            _context.Entry(existing).State = EntityState.Detached;
            _context.Loans.Update(loan);
            await _context.SaveChangesAsync();
            return loan;
        }

        public async Task<Loan?> TrackEmiAsync(int loanId, int emiPaid)// over here emi paid is number of emis paid at once 

        {
            var loan = await _context.Loans.FindAsync(loanId);
            if (loan == null) return null;

            loan.PaidEmiCount += emiPaid;
            //loan.RemainingAmount = Math.Max(0, loan.RemainingAmount - loan.EmiAmount * emiPaid);// used max to avoid negative values
            loan.RemainingAmount -= loan.EmiAmount * emiPaid;
           

            if (loan.PaidEmiCount >= loan.TotalEmiCount)
                loan.LoanStatus = LoanStatus.Completed;

            loan.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return loan;
        }

        public async Task<Loan?> DeleteAsync(int id)
        {
            var loan = await _context.Loans.FindAsync(id);
            if (loan == null) return null;

            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();
            return loan;
        }

    }

}
