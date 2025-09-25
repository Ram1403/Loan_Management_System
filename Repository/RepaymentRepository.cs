using System;
using Loan_Management_System.Models;
using Loan_Management_System.Repository;
using Microsoft.EntityFrameworkCore;
using Loan_Management_System.Data;
using Loan_Management_System.Services.Payment;

public class RepaymentRepository : IRepaymentRepository
{
    private readonly LoanDbContext _context;
    private readonly IPaymentGatewayService _paymentGateway;

    public RepaymentRepository(LoanDbContext context, IPaymentGatewayService paymentGateway)
    {
        _context = context;
        _paymentGateway = paymentGateway;
    }

    public async Task<IEnumerable<Repayment>> GetAllAsync() =>
     await _context.Repayments
         .Include(r => r.Loan)
         .ThenInclude(l => l.LoanApplication)
         .ToListAsync();

    public async Task<Repayment?> GetByIdAsync(int id) =>
        await _context.Repayments
            .Include(r => r.Loan)
            .FirstOrDefaultAsync(r => r.RepaymentId == id);

    public async Task<IEnumerable<Repayment>> GetByLoanIdAsync(int loanId) =>
        await _context.Repayments
            .Where(r => r.LoanId == loanId)
            .ToListAsync();

    public async Task<IEnumerable<Repayment>> GetByCustomerIdAsync(int customerId) =>
        await _context.Repayments
            .Include(r => r.Loan)
            .ThenInclude(l => l.LoanApplication)
            .Where(r => r.Loan.LoanApplication.CustomerId == customerId)
            .ToListAsync();

    public async Task<Repayment?> DeleteAsync(int id)
    {
        var repayment = await _context.Repayments.FindAsync(id);
        if (repayment == null) return null;

        _context.Repayments.Remove(repayment);
        await _context.SaveChangesAsync();
        return repayment;
    }

    public async Task<Repayment> CreateAsync(Repayment repayment)
    {
        // Process dummy payment first
        var (success, transactionId, response) = await _paymentGateway.ProcessPayment(repayment.Amount, repayment.PaymentMode);

        repayment.TransactionId = transactionId;
        repayment.PaymentStatus = success ? PaymentStatus.Completed : PaymentStatus.Failed;
        repayment.PaymentGatewayResponse = response;
        repayment.PaymentDate = DateTime.UtcNow;

        await _context.Repayments.AddAsync(repayment);

        // Update loan
        var loan = await _context.Loans.FindAsync(repayment.LoanId);
        if (loan != null && success)
        {
            loan.PaidEmiCount += 1;
            loan.RemainingAmount -= repayment.Amount;
            if (loan.PaidEmiCount >= loan.TotalEmiCount)
                loan.LoanStatus = LoanStatus.Completed;
        }

        await _context.SaveChangesAsync();
        return repayment;
    }


}
