using Loan_Management_System.Models;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    Task<Customer?> GetCustomerByIdAsync(int id);
    Task<Customer?> GetByUserId(int userId);
    Task<Customer> AddCustomerAsync(Customer customer);
    Task<Customer?> UpdateCustomerAsync(Customer customer);
    Task<Customer?> VerifyCustomerAsync(int id);
    Task<Customer?> RejectCustomerAsync(int id, string reason);
    Task<Customer?> DeleteCustomerAsync(int id);
}




//using Loan_Management_System.Models;
//namespace Loan_Management_System.Services
//{
//    public interface ICustomerService
//    {
//        Task<IEnumerable<Customer>> GetAll();
//        Task<Customer?> GetById(int id);
//        Task<Customer> Add(Customer customer);
//        Task<Customer> Update(Customer customer);
//        Task<Customer> Delete(int id);
//    }
//}
