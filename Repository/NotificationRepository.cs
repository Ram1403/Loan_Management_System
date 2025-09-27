using System;
using Loan_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Loan_Management_System.Data;

public class NotificationRepository : INotificationRepository
{
    private readonly LoanDbContext _context;
    public NotificationRepository(LoanDbContext context) { _context = context; }

    //public async Task<IEnumerable<EmailNotification>> GetAllAsync() =>
    //    await _context.EmailNotifications.Include(n => n.User).ToListAsync();

    public async Task<IEnumerable<EmailNotification>> GetByUserAsync(int userId) =>
        await _context.EmailNotifications.Where(n => n.UserId == userId).ToListAsync();

    public async Task<EmailNotification?> GetByIdAsync(int id) =>
     await _context.EmailNotifications
                   .Include(n => n.User)   // ensures User.Email is loaded
                   .FirstOrDefaultAsync(n => n.NotificationId == id);

    public async Task<IEnumerable<EmailNotification>> GetAllAsync() =>
        await _context.EmailNotifications
                      .Include(n => n.User)   //  also load here
                      .ToListAsync();

    public async Task<EmailNotification> CreateAsync(EmailNotification notification)
    {
        await _context.EmailNotifications.AddAsync(notification);
        await _context.SaveChangesAsync();
        return notification;
    }

    public async Task<bool> MarkAsSentAsync(int id)
    {
        var notification = await _context.EmailNotifications.FindAsync(id);
        if (notification == null) return false;
        notification.IsSent = true;
        notification.SentAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var notification = await _context.EmailNotifications.FindAsync(id);
        if (notification == null) return false;
        _context.EmailNotifications.Remove(notification);
        await _context.SaveChangesAsync();
        return true;
    }
}
