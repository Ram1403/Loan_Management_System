using Loan_Management_System.Models;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;

    public CustomerService(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync() =>
        await _repository.GetAllCustomersAsync();

    public async Task<Customer?> GetCustomerByIdAsync(int id) =>
        await _repository.GetCustomerByIdAsync(id);

    public Task<Customer?> GetByUserId(int userId) => _repository.GetByUserId(userId);

    public async Task<Customer> AddCustomerAsync(Customer customer) =>
        await _repository.AddCustomerAsync(customer);

    public async Task<Customer?> UpdateCustomerAsync(Customer customer) =>
        await _repository.UpdateCustomerAsync(customer);

    public async Task<Customer?> VerifyCustomerAsync(int id) =>
        await _repository.VerifyCustomerAsync(id);

    public async Task<Customer?> RejectCustomerAsync(int id, string reason) =>
        await _repository.RejectCustomerAsync(id, reason);

    public async Task<Customer?> DeleteCustomerAsync(int id) =>
        await _repository.DeleteCustomerAsync(id);
}
