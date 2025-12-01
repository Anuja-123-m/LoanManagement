using LoanManagementAPI.Models;

namespace LoanManagementAPI.Repository.CustomerRepo
{
    public interface ICustomerRepository
    {
        // Profile Management
        Task<Customer?> GetCustomerByUserIdAsync(string userId);
        Task<Customer> RegisterCustomerAsync(Customer customer);

        // Loan Request Management
        Task<List<LoanRequest>> GetLoanRequestsByCustomerIdAsync(int customerId);
        Task<LoanRequest?> GetLoanRequestByIdAsync(int id, int customerId);
        Task<LoanRequest> CreateLoanRequestAsync(LoanRequest loanRequest);
    }
}

