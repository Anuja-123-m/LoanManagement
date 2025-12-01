using Microsoft.EntityFrameworkCore;
using LoanManagementAPI.Database;
using LoanManagementAPI.Models;

namespace LoanManagementAPI.Repository.LoanOfficerRepo
{
    public class LoanOfficerRepository : ILoanOfficerRepository
    {
        private readonly LoanDbContext _context;

        public LoanOfficerRepository(LoanDbContext context)
        {
            _context = context;
        }

        // Profile Management
        public async Task<LoanOfficer?> GetOfficerByUserIdAsync(string userId)
        {
            return await _context.LoanOfficers
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.UserId == userId);
        }

        // Assigned Loan Requests
        public async Task<List<LoanRequest>> GetAssignedLoanRequestsAsync(int officerId)
        {
            return await _context.LoanRequests
                .Where(lr => lr.AssignedOfficerId == officerId)
                .Include(lr => lr.Customer)
                .ToListAsync();
        }

        public async Task<LoanRequest?> GetAssignedLoanRequestByIdAsync(int id, int officerId)
        {
            return await _context.LoanRequests
                .Include(lr => lr.Customer)
                .FirstOrDefaultAsync(lr => lr.LoanRequestId == id && lr.AssignedOfficerId == officerId);
        }

        // Background Verifications
        public async Task<List<BackgroundVerification>> GetBackgroundVerificationsByOfficerIdAsync(int officerId)
        {
            return await _context.BackgroundVerifications
                .Where(v => v.AssignedOfficerId == officerId)
                .Include(v => v.LoanRequest)
                .ToListAsync();
        }

        public async Task<BackgroundVerification?> GetBackgroundVerificationByIdAsync(int id, int officerId)
        {
            return await _context.BackgroundVerifications
                .Include(v => v.LoanRequest)
                .FirstOrDefaultAsync(v => v.VerificationId == id && v.AssignedOfficerId == officerId);
        }

        // Loan Verifications
        public async Task<List<LoanVerification>> GetLoanVerificationsByOfficerIdAsync(int officerId)
        {
            return await _context.LoanVerifications
                .Where(v => v.AssignedOfficerId == officerId)
                .Include(v => v.LoanRequest)
                .ToListAsync();
        }

        public async Task<LoanVerification?> GetLoanVerificationByIdAsync(int id, int officerId)
        {
            return await _context.LoanVerifications
                .Include(v => v.LoanRequest)
                .FirstOrDefaultAsync(v => v.VerificationId == id && v.AssignedOfficerId == officerId);
        }
    }
}

