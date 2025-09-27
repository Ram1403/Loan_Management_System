using System;
using Loan_Management_System.Models;
using Loan_Management_System.Repository;
using Microsoft.EntityFrameworkCore;
using Loan_Management_System.Data;

public class CustomerRepository : ICustomerRepository
{
    private readonly LoanDbContext _context;

    public CustomerRepository(LoanDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync() =>
        await _context.Customers.Include(c => c.User).ToListAsync();

    public async Task<Customer?> GetCustomerByIdAsync(int id) =>
        await _context.Customers.FindAsync(id);

    public async Task<Customer> AddCustomerAsync(Customer customer)
    {
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<Customer?> UpdateCustomerAsync(Customer customer)
    {
        var existing = await _context.Customers.FindAsync(customer.CustomerId);
        if (existing == null) return null;

        // Update allowed fields
        existing.FirstName = customer.FirstName;
        existing.LastName = customer.LastName;
        existing.DateOfBirth = customer.DateOfBirth;
        existing.Address = customer.Address;
        existing.City = customer.City;
        existing.Occupation = customer.Occupation;
        existing.AnnualIncome = customer.AnnualIncome;
        existing.CreditScore = customer.CreditScore;
        existing.Gender = customer.Gender;

        // Sensitive/identity fields - update only if needed
        existing.PanNumber = customer.PanNumber;
        existing.AadhaarNumber = customer.AadhaarNumber;
        existing.DocumentType = customer.DocumentType;
        existing.DocumentPath = customer.DocumentPath;

        existing.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return existing;
    }



    public async Task<Customer?> VerifyCustomerAsync(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null) return null;

        customer.VerificationStatus = VerificationStatus.Verified;
        customer.VerifiedAt = DateTime.UtcNow;
        customer.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<Customer?> RejectCustomerAsync(int id, string reason)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null) return null;

        customer.VerificationStatus = VerificationStatus.Rejected;
        customer.VerifiedAt = DateTime.UtcNow;   
        customer.UpdatedAt = DateTime.UtcNow;

        // ⚡ You don’t have a RejectionReason field in your model. 
        // If you want to store reason, you must add one:
        // public string? RejectionReason { get; set; }

        await _context.SaveChangesAsync();
        return customer;
    }


    public async Task<Customer?> DeleteCustomerAsync(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null) return null;

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
        return customer;
    }
}







//oldversion
//using System;
//using Loan_Management_System.Data;
//using Loan_Management_System.Models;
//using Microsoft.EntityFrameworkCore;
//namespace Loan_Management_System.Repository
//{
//    public class CustomerRepository:ICustomerRepository
//    {
//        private readonly LoanDbContext _context;

//        public CustomerRepository(LoanDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<IEnumerable<Customer>> GetAll() =>
//            await _context.Customers.Include(c => c.User).ToListAsync();

//        public async Task<Customer?> GetById(int id) =>
//            await _context.Customers.Include(c => c.User)
//                                    .FirstOrDefaultAsync(c => c.CustomerId == id);

//        public async Task <Customer>Add(Customer customer)
//        {
//            await _context.Customers.AddAsync(customer);
//            await _context.SaveChangesAsync();
//            return customer;
//        }

//        public async Task<Customer> Update(Customer customer)
//        {
//            _context.Customers.Update(customer);
//            await _context.SaveChangesAsync();
//            return customer;
//        }

//        public async Task<Customer> Delete(int id)
//        {
//            var customer = await GetById(id);
//            if (customer != null)
//            {
//                _context.Customers.Remove(customer);
//                await _context.SaveChangesAsync();
//            }
//            return customer;
//        }

//        public async Task<bool> ExistsAsync(int id) =>
//            await _context.Customers.AnyAsync(c => c.CustomerId == id);
//    }
//}
