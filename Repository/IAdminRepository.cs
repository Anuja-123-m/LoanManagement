using LoanManagementAPI.Models;

namespace LoanManagementAPI.Repository
{
    public interface IAdminRepository
    {
        // Customer Management
        Task<List<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task<bool> ApproveCustomerAsync(int id);
        Task<bool> RejectCustomerAsync(int id);

        // Loan Officer Management
        Task<List<LoanOfficer>> GetAllOfficersAsync();
        Task<LoanOfficer?> GetOfficerByIdAsync(int id);
        Task<LoanOfficer> AddOfficerAsync(LoanOfficer officer);
        Task<bool> ApproveOfficerAsync(int id);
        Task<bool> RejectOfficerAsync(int id);

        // Loan Request Management
        Task<List<LoanRequest>> GetAllLoanRequestsAsync();
        Task<LoanRequest?> GetLoanRequestByIdAsync(int id);

        // Background Verification
        Task<List<BackgroundVerification>> GetBackgroundVerificationsAsync();
        Task<BackgroundVerification?> GetBackgroundVerificationByIdAsync(int id);
        Task<BackgroundVerification> AddBackgroundVerificationAsync(BackgroundVerification verification);

        // Loan Verification
        Task<List<LoanVerification>> GetLoanVerificationsAsync();
        Task<LoanVerification?> GetLoanVerificationByIdAsync(int id);
        Task<LoanVerification> AddLoanVerificationAsync(LoanVerification verification);

        // Feedback Question
        Task<List<FeedbackQuestion>> GetFeedbackQuestionsAsync();
        Task<FeedbackQuestion?> GetFeedbackQuestionByIdAsync(int id);
        Task<FeedbackQuestion> AddFeedbackQuestionAsync(FeedbackQuestion question);

        // Customer Feedback
        Task<List<CustomerFeedback>> GetAllCustomerFeedbacksAsync();

        // Help Report
        Task<List<HelpReport>> GetHelpReportsAsync();
        Task<HelpReport?> GetHelpReportByIdAsync(int id);
        Task<HelpReport> AddHelpReportAsync(HelpReport report);
    }
}

