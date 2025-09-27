
using Loan_Management_System.Models;
using Loan_Management_System.Repository;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<User> CreateUserAsync(User user) =>
        await _repository.CreateAsync(user);

    public async Task<User?> UpdateUserAsync(User user) =>
        await _repository.UpdateAsync(user);

    public async Task<User?> DeleteUserAsync(int id) =>
        await _repository.DeleteAsync(id);

    public async Task<User?> GetUserByIdAsync(int id) =>
        await _repository.GetByIdAsync(id);

    public async Task<List<User>> GetAllUsersAsync() =>
        await _repository.GetAllAsync();

    public async Task<User?> UpdateUserRoleAsync(int id, Role role) =>
        await _repository.UpdateRoleAsync(id, role);

    public async Task<User?> GetProfileAsync(int id) =>
        await _repository.GetProfileAsync(id);
}

//using Loan_Management_System.Repository;
//using Loan_Management_System.Models;
//using System.Threading.Tasks;

//namespace Loan_Management_System.Services
//{
//    public class UserService : IUserService
//    {
//        private readonly IUserRepository _userRepository;

//        public UserService(IUserRepository userRepository)
//        {
//            _userRepository = userRepository;
//        }

//        public async Task<User?> GetById(int userId)
//        {
//            return await _userRepository.GetById(userId);
//        }
//        public async Task<User> Create(User user)
//        {

//            return await _userRepository.Create(user);
//        }

//        public async Task<User?> Update(User user)
//        {

//            return await _userRepository.Update(user);
//        }
//        public async Task<User?> Delete(int userId)
//        {
//            // Fix: Use Delete instead of DeleteUser, matching UserRepository signature
//            return await _userRepository.Delete(userId);
//        }
//        public async Task<List<User>> GetAll()
//        {
//            return await _userRepository.GetAll();
//        }

//    }
//}
