using Loan_Management_System.Models;
using Loan_Management_System.Repository;

public class CustomerQueryService : ICustomerQueryService
{
    private readonly ICustomerQueryRepository _repository;

    public CustomerQueryService(ICustomerQueryRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<CustomerQuery>> GetQueriesByCustomerAsync(int customerId) =>
        _repository.GetQueriesByCustomerAsync(customerId);

    public Task<CustomerQuery?> GetByIdAsync(int id) =>
        _repository.GetByIdAsync(id);

    public Task<CustomerQuery> CreateAsync(CustomerQuery query) =>
        _repository.CreateAsync(query);

    public Task<CustomerQuery?> RespondToQueryAsync(int queryId, string response, int officerId) =>
        _repository.RespondToQueryAsync(queryId, response, officerId);

    public Task<bool> DeleteAsync(int id) =>
        _repository.DeleteAsync(id);
}
