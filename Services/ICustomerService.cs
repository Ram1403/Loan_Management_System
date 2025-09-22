using Loan_Management_System.Models;
namespace Loan_Management_System.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer?> GetById(int id);
        Task<Customer> Add(Customer customer);
        Task<Customer> Update(Customer customer);
        Task<Customer> Delete(int id);
    }
}
