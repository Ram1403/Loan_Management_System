using Microsoft.EntityFrameworkCore;
using Loan_Management_System.Models;
namespace Loan_Management_System.Data
{
    public class LoanDbContext: DbContext
    {
        public LoanDbContext() { }
        public LoanDbContext(DbContextOptions<LoanDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<LoanOfficer> LoanOfficers { get; set; }
        public DbSet<LoanAdmin> LoanAdmins { get; set; }
        public DbSet<LoanApplication> LoanApplications { get; set; }
        public DbSet<LoanDocument> LoanDocuments { get; set; }
        public DbSet<LoanScheme> LoanSchemes { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Repayment> Repayments { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Npa> Npas { get; set; }
        public DbSet<CustomerQuery> CustomerQueries { get; set; }
        public DbSet<EmailNotification> EmailNotifications { get; set; }

        //for removing double casecade delete
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Customer → LoanApplications : keep cascade delete
            modelBuilder.Entity<LoanApplication>()
                .HasOne(l => l.Customer)
                .WithMany(c => c.LoanApplications)
                .HasForeignKey(l => l.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // LoanScheme → LoanApplications : remove cascade delete
            modelBuilder.Entity<LoanApplication>()
                .HasOne(l => l.LoanScheme)
                .WithMany(s => s.LoanApplications)
                .HasForeignKey(l => l.SchemeId)
                .OnDelete(DeleteBehavior.Restrict);  // or DeleteBehavior.NoAction
        }




    }
}
