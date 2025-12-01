using LoanManagementAPI.Models;

namespace LoanManagementAPI.Repository.BackgroundVerificationRepo
{
    public interface IBackgroundVerificationRepository
    {
        List<BackgroundVerification> GetBackgroundVerifications();
        BackgroundVerification? GetBackgroundVerificationById(int id);
        List<BackgroundVerification> GetBackgroundVerificationsByLoanRequestId(int loanRequestId);
        List<BackgroundVerification> GetBackgroundVerificationsByOfficerId(int officerId);
        void AddBackgroundVerification(BackgroundVerification verification);
        bool UpdateBackgroundVerification(int id, BackgroundVerification verification);
        bool DeleteBackgroundVerification(int id);
        bool AssignOfficer(int verificationId, int officerId);
    }
}

