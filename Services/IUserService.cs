using Loan_Management_System.Models;

public interface IUserService
{
    Task<User> CreateUserAsync(User user);
    Task<User?> UpdateUserAsync(User user);
    Task<User?> DeleteUserAsync(int id);
    Task<User?> GetUserByIdAsync(int id);
    Task<List<User>> GetAllUsersAsync();
    Task<User?> UpdateUserRoleAsync(int id, Role role);
    Task<User?> GetProfileAsync(int id);
}


//using Loan_Management_System.Models;

//namespace Loan_Management_System.Services
//{
//    public interface IUserService
//    {
//        Task<User> Create(User user);
//        Task<User> Update(User user);
//        Task<User> Delete(int id);
//        Task<User> GetById(int id);
//        Task<List<User>> GetAll();
//    }
//}
