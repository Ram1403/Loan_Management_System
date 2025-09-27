using Loan_Management_System.Models;

namespace Loan_Management_System.Repository
{
    public interface ICustomerQueryRepository
    {
        Task<IEnumerable<CustomerQuery>> GetQueriesByCustomerAsync(int customerId);
        Task<CustomerQuery?> RespondToQueryAsync(int queryId, string response, int officerId);
        Task<CustomerQuery?> GetByIdAsync(int id);
        Task<CustomerQuery> CreateAsync(CustomerQuery query);
        Task<bool> DeleteAsync(int id);
    }
}
