using Loan_Management_System.Models;
namespace Loan_Management_System.Repository
{
    public interface IUserRepository
    {
        Task<User> Create(User user);
        Task<User> Update(User user);
        Task<User> Delete(int id);
        Task<User> GetById(int id);
        Task<List<User>> GetAll();




    }
}
