using Loan_Management_System.Data;
using Loan_Management_System.Models;
using Microsoft.EntityFrameworkCore;
namespace Loan_Management_System.Repository
{
    public class AdminRepository: IAdminRepository
    {
        private readonly LoanDbContext _context;

        public AdminRepository(LoanDbContext context)
        {
            _context = context;
        }

        public async Task<LoanAdmin?> Create(LoanAdmin loanAdmin)
        {
            _context.LoanAdmins.Add(loanAdmin);
            await _context.SaveChangesAsync();
            return loanAdmin;

        }
         public async Task<LoanAdmin> Update(LoanAdmin loanAdmin)
        {
            var existingAdmin = await _context.LoanAdmins.FindAsync(loanAdmin.AdminId);
            if (existingAdmin != null)
            {
                
                existingAdmin.AdminLevel = loanAdmin.AdminLevel;
                existingAdmin.UserId = loanAdmin.UserId;
                existingAdmin.AppointedDate = loanAdmin.AppointedDate;
                existingAdmin.CreatedAt = loanAdmin.CreatedAt;
                existingAdmin.UpdatedAt = loanAdmin.UpdatedAt;
                await _context.SaveChangesAsync();
              

            }
            return existingAdmin;


        }
        public async Task<LoanAdmin?> Delete(int id)
        {
            var admin = await _context.LoanAdmins.FindAsync(id);
            if (admin == null) return null;
            _context.LoanAdmins.Remove(admin);
            await _context.SaveChangesAsync();
            return admin;
        }
        public async Task<LoanAdmin?> GetById(int id)
        {
            return await _context.LoanAdmins.Include(u=>u.User).FirstOrDefaultAsync(a=>a.AdminId==id);
        }
        public async Task<List<LoanAdmin>> GetAll()
        {
            return await _context.LoanAdmins.Include(u=>u.User).ToListAsync();
        }

        
    }
}
