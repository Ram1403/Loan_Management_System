using System;
using Loan_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Loan_Management_System.Data;

namespace Loan_Management_System.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly LoanDbContext _context;

        public AdminRepository(LoanDbContext context)
        {
            _context = context;
        }

        public async Task<LoanAdmin> CreateAsync(LoanAdmin loanAdmin)
        {
            await _context.LoanAdmins.AddAsync(loanAdmin);
            await _context.SaveChangesAsync();
            return loanAdmin;
        }

        public async Task<LoanAdmin?> UpdateAsync(LoanAdmin loanAdmin)
        {
            var existing = await _context.LoanAdmins.FindAsync(loanAdmin.AdminId);
            if (existing == null) return null;

            existing.AdminLevel = loanAdmin.AdminLevel;
            existing.AppointedDate = loanAdmin.AppointedDate;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<LoanAdmin?> DeleteAsync(int id)
        {
            var admin = await _context.LoanAdmins.FindAsync(id);
            if (admin == null) return null;

            _context.LoanAdmins.Remove(admin);
            await _context.SaveChangesAsync();
            return admin;
        }

        public async Task<LoanAdmin?> GetByIdAsync(int id) =>
            await _context.LoanAdmins
                .Include(a => a.User)
                .Include(a => a.LoanSchemes)
                .FirstOrDefaultAsync(a => a.AdminId == id);

        public async Task<List<LoanAdmin>> GetAllAsync() =>
            await _context.LoanAdmins.Include(a => a.User).ToListAsync();

        public async Task<LoanScheme> CreateLoanSchemeAsync(int adminId, LoanScheme scheme)
        {
            scheme.CreatedBy = adminId;
            scheme.CreatedAt = DateTime.UtcNow;
            await _context.LoanSchemes.AddAsync(scheme);
            await _context.SaveChangesAsync();
            return scheme;
        }

        public async Task<LoanScheme?> UpdateLoanSchemeAsync(int schemeId, LoanScheme scheme)
        {
            var existing = await _context.LoanSchemes.FindAsync(schemeId);
            if (existing == null) return null;

            existing.SchemeName = scheme.SchemeName;
            existing.InterestRate = scheme.InterestRate;
            existing.MaxAmount = scheme.MaxAmount;
            existing.MinAmount = scheme.MinAmount;
            existing.TenureMonths = scheme.TenureMonths;
            existing.EligibilityCriteria = scheme.EligibilityCriteria;
            existing.IsActive = scheme.IsActive;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteLoanSchemeAsync(int schemeId)
        {
            var scheme = await _context.LoanSchemes.FindAsync(schemeId);
            if (scheme == null) return false;

            _context.LoanSchemes.Remove(scheme);
            await _context.SaveChangesAsync();
            return true;
        }



        // ✅ Assign Officer to Loan Application
        public async Task<LoanApplication?> AssignOfficerToApplicationAsync(int applicationId, int officerId)
        {
            var application = await _context.LoanApplications.FindAsync(applicationId);
            if (application == null) return null;

            var officer = await _context.LoanOfficers.FindAsync(officerId);
            if (officer == null || !officer.IsActive) return null;

            application.OfficerId = officerId;
            application.UpdatedAt = DateTime.UtcNow;

            officer.CurrentWorkload += 1;
            officer.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return application;
        }

        //make change this
        //public async Task<LoanOfficer?> AssignOfficerAsync(int officerId)
        //{
        //    var officer = await _context.LoanOfficers.FindAsync(officerId);
        //    if (officer == null) return null;

        //    // Example: simulate workload increment
        //    officer.CurrentWorkload += 1;
        //    officer.UpdatedAt = DateTime.UtcNow;

        //    await _context.SaveChangesAsync();
        //    return officer;
        //}

        public async Task<LoanAdmin?> DeactivateAsync(int adminId)
        {
            var admin = await _context.LoanAdmins.FindAsync(adminId);
            if (admin == null) return null;

            admin.AdminLevel = "Inactive";
            admin.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return admin;
        }

        public async Task<LoanAdmin?> ReactivateAsync(int adminId)
        {
            var admin = await _context.LoanAdmins.FindAsync(adminId);
            if (admin == null) return null;

            admin.AdminLevel = "Active";
            admin.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return admin;
        }

        public async Task<List<string>> GetAuditLogAsync(int adminId)
        {
            return new List<string>
            {
                $"Admin {adminId} created a scheme at {DateTime.UtcNow}",
                $"Admin {adminId} assigned officer at {DateTime.UtcNow}"
            };
        }
    }
}



//using Loan_Management_System.Data;
//using Loan_Management_System.Models;
//using Microsoft.EntityFrameworkCore;
//namespace Loan_Management_System.Repository
//{
//    public class AdminRepository: IAdminRepository
//    {
//        private readonly LoanDbContext _context;

//        public AdminRepository(LoanDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<LoanAdmin?> Create(LoanAdmin loanAdmin)
//        {
//            _context.LoanAdmins.Add(loanAdmin);
//            await _context.SaveChangesAsync();
//            return loanAdmin;

//        }
//         public async Task<LoanAdmin> Update(LoanAdmin loanAdmin)
//        {
//            var existingAdmin = await _context.LoanAdmins.FindAsync(loanAdmin.AdminId);
//            if (existingAdmin != null)
//            {

//                existingAdmin.AdminLevel = loanAdmin.AdminLevel;
//                existingAdmin.UserId = loanAdmin.UserId;
//                existingAdmin.AppointedDate = loanAdmin.AppointedDate;
//                existingAdmin.CreatedAt = loanAdmin.CreatedAt;
//                existingAdmin.UpdatedAt = loanAdmin.UpdatedAt;
//                await _context.SaveChangesAsync();


//            }
//            return existingAdmin;


//        }
//        public async Task<LoanAdmin?> Delete(int id)
//        {
//            var admin = await _context.LoanAdmins.FindAsync(id);
//            if (admin == null) return null;
//            _context.LoanAdmins.Remove(admin);
//            await _context.SaveChangesAsync();
//            return admin;
//        }
//        public async Task<LoanAdmin?> GetById(int id)
//        {
//            return await _context.LoanAdmins.Include(u=>u.User).FirstOrDefaultAsync(a=>a.AdminId==id);
//        }
//        public async Task<List<LoanAdmin>> GetAll()
//        {
//            return await _context.LoanAdmins.Include(u=>u.User).ToListAsync();
//        }


//    }
//}
