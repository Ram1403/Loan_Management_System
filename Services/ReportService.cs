using Loan_Management_System.Models;
using Loan_Management_System.Repository;

public class ReportService : IReportService
{
    private readonly IReportRepository _repository;

    public ReportService(IReportRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Report>> GetAllAsync() =>
        await _repository.GetAllAsync();

    public async Task<Report?> GetByIdAsync(int id) =>
        await _repository.GetByIdAsync(id);

    public async Task<IEnumerable<Report>> GetByAdminAsync(int adminId) =>
        await _repository.GetByAdminAsync(adminId);

    public async Task<Report> CreateAsync(Report report) =>
        await _repository.CreateAsync(report);

    public async Task<bool> DeleteAsync(int id) =>
        await _repository.DeleteAsync(id);
}
