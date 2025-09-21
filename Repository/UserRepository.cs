using Microsoft.EntityFrameworkCore;
using Loan_Management_System.Data;
using Loan_Management_System.Models;

namespace Loan_Management_System.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly LoanDbContext _context;

        public UserRepository(LoanDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetById(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<User> Create(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        //make changes here by getting vales from existing user and updating it with new user values//
        public async Task<User?> Update(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.UserId);
            if (existingUser == null) return null;

            _context.Entry(existingUser).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();
            return existingUser;
        }

        public async Task<User?> Delete(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return null;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }
    }
}