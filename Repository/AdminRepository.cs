using Microsoft.EntityFrameworkCore;
using LoanManagementAPI.Database;
using LoanManagementAPI.Models;


namespace LoanManagementAPI.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly LoanDbContext _context;

        public AdminRepository(LoanDbContext context)
        {
            _context = context;
        }

        // Customer Management
        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.Include(c => c.User).ToListAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await _context.Customers.Include(c => c.User).FirstOrDefaultAsync(c => c.CustomerId == id);
        }

        public async Task<bool> ApproveCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return false;
            customer.Status = "Approved";
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RejectCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return false;
            customer.Status = "Rejected";
            await _context.SaveChangesAsync();
            return true;
        }

        // Loan Officer Management
        public async Task<List<LoanOfficer>> GetAllOfficersAsync()
        {
            return await _context.LoanOfficers.Include(o => o.User).ToListAsync();
        }

        public async Task<LoanOfficer?> GetOfficerByIdAsync(int id)
        {
            return await _context.LoanOfficers.Include(o => o.User).FirstOrDefaultAsync(o => o.OfficerId == id);
        }

        public async Task<LoanOfficer> AddOfficerAsync(LoanOfficer officer)
        {
            _context.LoanOfficers.Add(officer);
            await _context.SaveChangesAsync();
            return officer;
        }

        public async Task<bool> ApproveOfficerAsync(int id)
        {
            var officer = await _context.LoanOfficers.FindAsync(id);
            if (officer == null) return false;
            officer.Status = "Approved";
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RejectOfficerAsync(int id)
        {
            var officer = await _context.LoanOfficers.FindAsync(id);
            if (officer == null) return false;
            officer.Status = "Rejected";
            await _context.SaveChangesAsync();
            return true;
        }

        // Loan Request Management
        public async Task<List<LoanRequest>> GetAllLoanRequestsAsync()
        {
            return await _context.LoanRequests
                .Include(lr => lr.Customer)
                .Include(lr => lr.AssignedOfficer)
                .ToListAsync();
        }

        public async Task<LoanRequest?> GetLoanRequestByIdAsync(int id)
        {
            return await _context.LoanRequests
                .Include(lr => lr.Customer)
                .Include(lr => lr.AssignedOfficer)
                .FirstOrDefaultAsync(lr => lr.LoanRequestId == id);
        }

        // Background Verification
        public async Task<List<BackgroundVerification>> GetBackgroundVerificationsAsync()
        {
            return await _context.BackgroundVerifications
                .Include(v => v.LoanRequest)
                .Include(v => v.AssignedOfficer)
                .ToListAsync();
        }

        public async Task<BackgroundVerification?> GetBackgroundVerificationByIdAsync(int id)
        {
            return await _context.BackgroundVerifications
                .Include(v => v.LoanRequest)
                .Include(v => v.AssignedOfficer)
                .FirstOrDefaultAsync(v => v.VerificationId == id);
        }

        public async Task<BackgroundVerification> AddBackgroundVerificationAsync(BackgroundVerification verification)
        {
            _context.BackgroundVerifications.Add(verification);
            await _context.SaveChangesAsync();
            return verification;
        }

        // Loan Verification
        public async Task<List<LoanVerification>> GetLoanVerificationsAsync()
        {
            return await _context.LoanVerifications
                .Include(v => v.LoanRequest)
                .Include(v => v.AssignedOfficer)
                .ToListAsync();
        }

        public async Task<LoanVerification?> GetLoanVerificationByIdAsync(int id)
        {
            return await _context.LoanVerifications
                .Include(v => v.LoanRequest)
                .Include(v => v.AssignedOfficer)
                .FirstOrDefaultAsync(v => v.VerificationId == id);
        }

        public async Task<LoanVerification> AddLoanVerificationAsync(LoanVerification verification)
        {
            _context.LoanVerifications.Add(verification);
            await _context.SaveChangesAsync();
            return verification;
        }

        // Feedback Question
        public async Task<List<FeedbackQuestion>> GetFeedbackQuestionsAsync()
        {
            return await _context.FeedbackQuestions.ToListAsync();
        }

        public async Task<FeedbackQuestion?> GetFeedbackQuestionByIdAsync(int id)
        {
            return await _context.FeedbackQuestions.FindAsync(id);
        }

        public async Task<FeedbackQuestion> AddFeedbackQuestionAsync(FeedbackQuestion question)
        {
            _context.FeedbackQuestions.Add(question);
            await _context.SaveChangesAsync();
            return question;
        }

        // Customer Feedback
        public async Task<List<CustomerFeedback>> GetAllCustomerFeedbacksAsync()
        {
            return await _context.CustomerFeedbacks
                .Include(f => f.Customer)
                .Include(f => f.FeedbackQuestion)
                .ToListAsync();
        }

        // Help Report
        public async Task<List<HelpReport>> GetHelpReportsAsync()
        {
            return await _context.HelpReports.ToListAsync();
        }

        public async Task<HelpReport?> GetHelpReportByIdAsync(int id)
        {
            return await _context.HelpReports.FindAsync(id);
        }

        public async Task<HelpReport> AddHelpReportAsync(HelpReport report)
        {
            _context.HelpReports.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }
    }
}

