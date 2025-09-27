using System;
using Loan_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Loan_Management_System.Data;

namespace Loan_Management_System.Repository
{
    public class CustomerQueryRepository : ICustomerQueryRepository
    {
        private readonly LoanDbContext _context;

        public CustomerQueryRepository(LoanDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerQuery>> GetQueriesByCustomerAsync(int customerId) =>
            await _context.CustomerQueries
                .Where(q => q.CustomerId == customerId)
                .ToListAsync();

        public async Task<CustomerQuery?> GetByIdAsync(int id) =>
            await _context.CustomerQueries.FindAsync(id);

        public async Task<CustomerQuery> CreateAsync(CustomerQuery query)
        {
            await _context.CustomerQueries.AddAsync(query);
            await _context.SaveChangesAsync();
            return query;
        }

        public async Task<CustomerQuery?> RespondToQueryAsync(int queryId, string response, int officerId)
        {
            var query = await _context.CustomerQueries.FindAsync(queryId);
            if (query == null) return null;

            var officer = await _context.LoanOfficers.FindAsync(officerId);
            if (officer == null) throw new InvalidOperationException($"Officer with ID {officerId} does not exist");

            query.OfficerId = officerId;
            query.OfficerResponse = response;
            //query.QueryStatus = QueryStatus.Resolved;
            query.QueryStatus = QueryStatus.Resolved;
            query.ResolvedAt = DateTime.UtcNow;
            query.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return query;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var query = await _context.CustomerQueries.FindAsync(id);
            if (query == null) return false;

            _context.CustomerQueries.Remove(query);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
