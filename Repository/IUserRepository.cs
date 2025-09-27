using Loan_Management_System.Models;

namespace Loan_Management_System.Repository
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<User?> UpdateAsync(User user);
        Task<User?> DeleteAsync(int id);
        Task<User?> GetByIdAsync(int id);
        Task<List<User>> GetAllAsync();
        Task<User?> UpdateRoleAsync(int id, Role role);
        Task<User?> GetProfileAsync(int id);
    }
}






//using Loan_Management_System.Models;
//namespace Loan_Management_System.Repository
//{
//    public interface IUserRepository
//    {
//        Task<User> Create(User user);
//        Task<User> Update(User user);
//        Task<User> Delete(int id);
//        Task<User> GetById(int id);
//        Task<List<User>> GetAll();




//    }
//}
