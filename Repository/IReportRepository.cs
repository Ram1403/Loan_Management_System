using Loan_Management_System.Models;

namespace Loan_Management_System.Repository
{
    public interface IReportRepository
    {
        Task<IEnumerable<Report>> GetAllAsync();
        Task<Report?> GetByIdAsync(int id);
        Task<IEnumerable<Report>> GetByAdminAsync(int adminId);
        Task<Report> CreateAsync(Report report);
        Task<bool> DeleteAsync(int id);
    }
}
