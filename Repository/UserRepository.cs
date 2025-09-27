using System;
using Loan_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Loan_Management_System.Data;

namespace Loan_Management_System.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly LoanDbContext _context;

        public UserRepository(LoanDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> UpdateAsync(User user)
        {
            var existing = await _context.Users.FindAsync(user.UserId);
            if (existing == null) return null;

            existing.Username = user.Username;
            existing.Email = user.Email;
            existing.PasswordHash = user.PasswordHash;
            existing.PhoneNumber = user.PhoneNumber;
            existing.UpdatedAt = DateTime.UtcNow;
            existing.UpdatedBy = user.UpdatedBy;
            existing.Role = user.Role;
            existing.IsDeleted = user.IsDeleted;



            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<User?> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            user.IsDeleted = true;
            user.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetByIdAsync(int id) =>
            await _context.Users.FirstOrDefaultAsync(u => u.UserId == id && !u.IsDeleted);

        public async Task<List<User>> GetAllAsync() =>
            await _context.Users.Where(u => !u.IsDeleted).ToListAsync();

        public async Task<User?> UpdateRoleAsync(int id, Role role)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            user.Role = role;
            user.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetProfileAsync(int id) =>
            await _context.Users.FirstOrDefaultAsync(u => u.UserId == id && !u.IsDeleted);
    }
}



//using Microsoft.EntityFrameworkCore;
//using Loan_Management_System.Data;
//using Loan_Management_System.Models;

//namespace Loan_Management_System.Repository
//{
//    public class UserRepository: IUserRepository
//    {
//        private readonly LoanDbContext _context;

//        public UserRepository(LoanDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<User?> GetById(int userId)
//        {
//            return await _context.Users.FindAsync(userId);
//        }

//        public async Task<User> Create(User user)
//        {
//            _context.Users.Add(user);
//            await _context.SaveChangesAsync();
//            return user;
//        }
//        //make changes here by getting vales from existing user and updating it with new user values//
//        public async Task<User?> Update(User user)
//        {
//            var existingUser = await _context.Users.FindAsync(user.UserId);
//            if (existingUser == null) return null;

//            _context.Entry(existingUser).CurrentValues.SetValues(user);
//            await _context.SaveChangesAsync();
//            return existingUser;
//        }

//        public async Task<User?> Delete(int userId)
//        {
//            var user = await _context.Users.FindAsync(userId);
//            if (user == null) return null;

//            _context.Users.Remove(user);
//            await _context.SaveChangesAsync();
//            return user;
//        }

//        public async Task<List<User>> GetAll()
//        {
//            return await _context.Users.ToListAsync();
//        }
//    }
//}