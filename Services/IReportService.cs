using Loan_Management_System.Models;

public interface IReportService
{
    Task<IEnumerable<Report>> GetAllAsync();
    Task<Report?> GetByIdAsync(int id);
    Task<IEnumerable<Report>> GetByAdminAsync(int adminId);
    Task<Report> CreateAsync(Report report);
    Task<bool> DeleteAsync(int id);
}
