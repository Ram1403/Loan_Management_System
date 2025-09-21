using Loan_Management_System.Repository;
using Loan_Management_System.Models;
using System.Threading.Tasks;

namespace Loan_Management_System.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> GetById(int userId)
        {
            return await _userRepository.GetById(userId);
        }
        public async Task<User> Create(User user)
        {
           
            return await _userRepository.Create(user);
        }

        public async Task<User?> Update(User user)
        {
            
            return await _userRepository.Update(user);
        }
        public async Task<User?> Delete(int userId)
        {
            // Fix: Use Delete instead of DeleteUser, matching UserRepository signature
            return await _userRepository.Delete(userId);
        }
        public async Task<List<User>> GetAll()
        {
            return await _userRepository.GetAll();
        }

    }
}
