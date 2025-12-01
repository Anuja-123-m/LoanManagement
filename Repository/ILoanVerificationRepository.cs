using LoanManagementAPI.Models;

namespace LoanManagementAPI.Repository.LoanVerificationRepo
{
    public interface ILoanVerificationRepository
    {
        List<LoanVerification> GetLoanVerifications();
        LoanVerification? GetLoanVerificationById(int id);
        List<LoanVerification> GetLoanVerificationsByLoanRequestId(int loanRequestId);
        List<LoanVerification> GetLoanVerificationsByOfficerId(int officerId);
        void AddLoanVerification(LoanVerification verification);
        bool UpdateLoanVerification(int id, LoanVerification verification);
        bool DeleteLoanVerification(int id);
        bool AssignOfficer(int verificationId, int officerId);
    }
}

