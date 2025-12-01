using Microsoft.EntityFrameworkCore;
using LoanManagementAPI.Database;
using LoanManagementAPI.Models;

namespace LoanManagementAPI.Repository.CustomerRepo
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly LoanDbContext _context;

        public CustomerRepository(LoanDbContext context)
        {
            _context = context;
        }

        // Profile Management
        public async Task<Customer?> GetCustomerByUserIdAsync(string userId)
        {
            return await _context.Customers
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<Customer> RegisterCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        // Loan Request Management
        public async Task<List<LoanRequest>> GetLoanRequestsByCustomerIdAsync(int customerId)
        {
            return await _context.LoanRequests
                .Where(lr => lr.CustomerId == customerId)
                .Include(lr => lr.AssignedOfficer)
                .ToListAsync();
        }

        public async Task<LoanRequest?> GetLoanRequestByIdAsync(int id, int customerId)
        {
            return await _context.LoanRequests
                .Include(lr => lr.AssignedOfficer)
                .FirstOrDefaultAsync(lr => lr.LoanRequestId == id && lr.CustomerId == customerId);
        }

        public async Task<LoanRequest> CreateLoanRequestAsync(LoanRequest loanRequest)
        {
            loanRequest.Status = "Pending";
            loanRequest.RequestDate = DateTime.Now;
            _context.LoanRequests.Add(loanRequest);
            await _context.SaveChangesAsync();
            return loanRequest;
        }
    }
}

