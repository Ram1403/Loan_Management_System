using System;
using Loan_Management_System.DTOs;
using Loan_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Loan_Management_System.Data;

namespace Loan_Management_System.Repository
{
    public class LoanApplicationRepository : ILoanApplicationRepository
    {
        private readonly LoanDbContext _context;

        public LoanApplicationRepository(LoanDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LoanApplication>> GetAllAsync() =>
            await _context.LoanApplications
                .Include(a => a.Customer)
                .Include(a => a.LoanScheme)
                .Include(a => a.LoanOfficer)
                .ToListAsync();

        public async Task<LoanApplication?> GetByIdAsync(int id) =>
            await _context.LoanApplications
                .Include(a => a.Customer)
                .Include(a => a.LoanScheme)
                .Include(a => a.LoanOfficer)
                .FirstOrDefaultAsync(a => a.ApplicationId == id);


        public async Task<IEnumerable<LoanApplication>> GetByCustomerAsync(int customerId) =>
    await _context.LoanApplications
        .Include(a => a.Customer)
        .Where(a => a.CustomerId == customerId)
        .ToListAsync();


        public async Task<LoanApplication> CreateAsync(LoanApplication application)
        {
            await _context.LoanApplications.AddAsync(application);
            await _context.SaveChangesAsync();
            return application;
        }

        public async Task<LoanApplication?> UpdateStatusAsync(int id, Status newStatus, string? remarks)
        {
            var application = await _context.LoanApplications.FindAsync(id);
            if (application == null) return null;

            application.Status = newStatus;
            application.Remarks = remarks;
            application.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return application;
        }

        public async Task<LoanApplication?> AssignOfficerAsync(int id, int officerId)
        {
            var application = await _context.LoanApplications.FindAsync(id);
            if (application == null) return null;

            application.OfficerId = officerId;
            application.OfficerAssignedDate = DateTime.UtcNow;
            application.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return application;
        }

        // ✅ New methods
        public async Task<bool> DeleteAsync(int id)
        {
            var application = await _context.LoanApplications.FindAsync(id);
            if (application == null) return false;

            _context.LoanApplications.Remove(application);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<LoanApplication?> UpdateAsync(LoanApplication application)
        {
            var existing = await _context.LoanApplications.FindAsync(application.ApplicationId);
            if (existing == null) return null;

            _context.Entry(existing).State = EntityState.Detached;
            _context.LoanApplications.Update(application);
            await _context.SaveChangesAsync();
            return application;
        }

        public async Task<IEnumerable<LoanApplication>> GetByStatusAsync(Status status) =>
            await _context.LoanApplications.Where(a => a.Status == status).ToListAsync();

        public async Task<IEnumerable<LoanApplication>> GetByDateRangeAsync(DateTime start, DateTime end) =>
            await _context.LoanApplications
                .Where(a => a.ApplicationDate >= start && a.ApplicationDate <= end)
                .ToListAsync();

        public async Task<IEnumerable<LoanApplication>> SearchAsync(string query) =>
            await _context.LoanApplications
                .Include(a => a.Customer)
                .Where(a => a.Customer.FirstName.Contains(query) ||
                            a.Customer.LastName.Contains(query) ||
                            a.Customer.CustomerId.ToString() == query)
                .ToListAsync();

        public async Task<IEnumerable<LoanApplication>> GetRecentByCustomerAsync(int customerId) =>
            await _context.LoanApplications
                .Where(a => a.CustomerId == customerId)
                .OrderByDescending(a => a.ApplicationDate)
                .Take(5)
                .ToListAsync();

        public async Task<IEnumerable<LoanApplication>> GetByCustomerAndStatusAsync(int customerId, Status status) =>
            await _context.LoanApplications
                .Where(a => a.CustomerId == customerId && a.Status == status)
                .ToListAsync();

        public async Task<ApplicationSummary> GetSummaryByCustomerAsync(int customerId)
        {
            var applications = await _context.LoanApplications
                .Where(a => a.CustomerId == customerId)
                .ToListAsync();

            return new ApplicationSummary
            {
                Total = applications.Count,
                Approved = applications.Count(a => a.Status == Status.Approved),
                Rejected = applications.Count(a => a.Status == Status.Rejected),
                Pending = applications.Count(a => a.Status == Status.Pending),
                Disbursed = applications.Count(a => a.Status == Status.Disbursed)
            };
        }
    }
}





//onld one without angular compatble

//using System;
//using Loan_Management_System.Data;
//using Loan_Management_System.Models;
//using Microsoft.EntityFrameworkCore;

//namespace Loan_Management_System.Repository
//{
//    public class LoanApplicationRepository : ILoanApplicationRepository
//    {
//        private readonly LoanDbContext _context;

//        public LoanApplicationRepository(LoanDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<IEnumerable<LoanApplication>> GetAllAsync() {
//            var application =  await _context.LoanApplications
//                .Include(a => a.Customer)
//                .Include(a => a.LoanScheme)
//                .Include(a => a.LoanOfficer)
//                .ToListAsync();
//                return application;
//        }

//        public async Task<LoanApplication?> GetByIdAsync(int id)
//        {
//            var application= await _context.LoanApplications
//                .Include(a => a.Customer)
//                .Include(a => a.LoanScheme)
//                .Include(a => a.LoanOfficer)
//                .FirstOrDefaultAsync(a => a.ApplicationId == id);
//            return application;
//        }


//        public async Task<LoanApplication> CreateAsync(LoanApplication application)
//        {
//            await _context.LoanApplications.AddAsync(application);
//            await _context.SaveChangesAsync();
//            return application;
//        }

//        public async Task<LoanApplication?> UpdateStatusAsync(int id, Status newStatus, string? remarks)
//        {
//            var application = await _context.LoanApplications.FindAsync(id);
//            if (application == null) return null;

//            application.Status = newStatus;
//            application.Remarks = remarks;
//            application.UpdatedAt = DateTime.UtcNow;

//            await _context.SaveChangesAsync();
//            return application;
//        }

//        public async Task<LoanApplication?> AssignOfficerAsync(int id, int officerId)
//        {
//            var application = await _context.LoanApplications.FindAsync(id);
//            if (application == null) return null;

//            application.OfficerId = officerId;
//            application.OfficerAssignedDate = DateTime.UtcNow;
//            application.UpdatedAt = DateTime.UtcNow;

//            await _context.SaveChangesAsync();
//            return application;
//        }
//    }

//}
