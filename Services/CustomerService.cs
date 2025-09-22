using Loan_Management_System.Models;
using Loan_Management_System.Data;
using Loan_Management_System.Repository;
namespace Loan_Management_System.Services
{
    public class CustomerService: ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Customer>> GetAll() =>
            await _repository.GetAll();

        
        public async Task<Customer?> GetById(int id) {
           var customer= await _repository.GetById(id);
            return customer;
        }
           

        public async Task <Customer>Add(Customer customer) {
            await _repository.Add(customer);
            return customer;
        }
           

        public async Task<Customer> Update(Customer customer) {
            await _repository.Update(customer);
            return customer;
        }
           

        public async Task<Customer> Delete(int id) {
           var customer= await _repository.Delete(id);
            return customer;
        }
            
    }
}
