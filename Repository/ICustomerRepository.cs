using Loan_Management_System.Models;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    Task<Customer?> GetCustomerByIdAsync(int id);
    Task<Customer> GetByUserId(int userId);
    Task<Customer> AddCustomerAsync(Customer customer);
    Task<Customer?> UpdateCustomerAsync(Customer customer);
    Task<Customer?> VerifyCustomerAsync(int id);
    Task<Customer?> RejectCustomerAsync(int id, string reason);
    Task<Customer?> DeleteCustomerAsync(int id);
}
