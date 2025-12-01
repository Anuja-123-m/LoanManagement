using LoanManagementAPI.Models;

namespace LoanManagementAPI.Repository.LoanOfficerRepo
{
    public interface ILoanOfficerRepository
    {
        // Profile Management
        Task<LoanOfficer?> GetOfficerByUserIdAsync(string userId);

        // Assigned Loan Requests
        Task<List<LoanRequest>> GetAssignedLoanRequestsAsync(int officerId);
        Task<LoanRequest?> GetAssignedLoanRequestByIdAsync(int id, int officerId);

        // Background Verifications
        Task<List<BackgroundVerification>> GetBackgroundVerificationsByOfficerIdAsync(int officerId);
        Task<BackgroundVerification?> GetBackgroundVerificationByIdAsync(int id, int officerId);

        // Loan Verifications
        Task<List<LoanVerification>> GetLoanVerificationsByOfficerIdAsync(int officerId);
        Task<LoanVerification?> GetLoanVerificationByIdAsync(int id, int officerId);
    }
}

