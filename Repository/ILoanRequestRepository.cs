using LoanManagementAPI.Models;

namespace LoanManagementAPI.Repository.LoanRequestRepo
{
    public interface ILoanRequestRepository
    {
        List<LoanRequest> GetLoanRequests();
        LoanRequest? GetLoanRequestById(int id);
        List<LoanRequest> GetLoanRequestsByCustomerId(int customerId);
        List<LoanRequest> GetLoanRequestsByOfficerId(int officerId);
        void AddLoanRequest(LoanRequest loanRequest);
        bool UpdateLoanRequest(int id, LoanRequest loanRequest);
        bool DeleteLoanRequest(int id);
        bool AssignOfficerForVerification(int loanRequestId, int officerId);
        bool AssignOfficerForLoanVerification(int loanRequestId, int officerId);
    }
}

