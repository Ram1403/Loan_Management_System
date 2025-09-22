using System;
using Loan_Management_System.Data;
using Loan_Management_System.Models;
using Microsoft.EntityFrameworkCore;
namespace Loan_Management_System.Repository
{
    public class CustomerRepository:ICustomerRepository
    {
        private readonly LoanDbContext _context;

        public CustomerRepository(LoanDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAll() =>
            await _context.Customers.Include(c => c.User).ToListAsync();

        public async Task<Customer?> GetById(int id) =>
            await _context.Customers.Include(c => c.User)
                                    .FirstOrDefaultAsync(c => c.CustomerId == id);

        public async Task <Customer>Add(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> Update(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> Delete(int id)
        {
            var customer = await GetById(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
            return customer;
        }

        //public async Task<bool> ExistsAsync(int id) =>
        //    await _context.Customers.AnyAsync(c => c.CustomerId == id);
    }
}
