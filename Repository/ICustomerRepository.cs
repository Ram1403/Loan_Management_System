using Microsoft.EntityFrameworkCore;
using Loan_Management_System.Models;
using Loan_Management_System.Data;
namespace Loan_Management_System.Repository
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer?> GetById(int id);
        Task <Customer>Add(Customer customer);
        Task<Customer> Update(Customer customer);
        Task <Customer>Delete(int id);
        //Task<bool> ExistsAsync(int id);
    }
}
