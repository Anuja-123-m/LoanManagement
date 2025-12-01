using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LoanManagementAPI.Models;

namespace LoanManagementAPI.Database
{
    public class LoanDbContext : IdentityDbContext<User>
    {
        public LoanDbContext(DbContextOptions<LoanDbContext> options) : base(options)
        {
        }

        // Make all DbSet properties non-nullable with default initialization
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<LoanOfficer> LoanOfficers { get; set; } = null!;
        public DbSet<LoanRequest> LoanRequests { get; set; } = null!;
        public DbSet<BackgroundVerification> BackgroundVerifications { get; set; } = null!;
        public DbSet<LoanVerification> LoanVerifications { get; set; } = null!;
        public DbSet<HelpReport> HelpReports { get; set; } = null!;
        public DbSet<FeedbackQuestion> FeedbackQuestions { get; set; } = null!;
        public DbSet<CustomerFeedback> CustomerFeedbacks { get; set; } = null!;
    }
}
