using Microsoft.EntityFrameworkCore;
using LoanManagementAPI.Database;
using LoanManagementAPI.Models;

namespace LoanManagementAPI.Repository.LoanRequestRepo
{
    public class LoanRequestRepository : ILoanRequestRepository
    {
        LoanDbContext loanDbContext;
        public LoanRequestRepository(LoanDbContext loanDbContext)
        {
            this.loanDbContext = loanDbContext;
        }

        public void AddLoanRequest(LoanRequest loanRequest)
        {
            try
            {
                loanDbContext.LoanRequests.Add(loanRequest);
                loanDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                string errorMessage = "Error adding loan request to database: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public bool AssignOfficerForLoanVerification(int loanRequestId, int officerId)
        {
            try
            {
                var loanRequest = loanDbContext.LoanRequests.FirstOrDefault(lr => lr.LoanRequestId == loanRequestId);
                if (loanRequest != null)
                {
                    loanRequest.AssignedOfficerId = officerId;
                    loanRequest.Status = "UnderVerification";
                    loanDbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error assigning officer for loan verification: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public bool AssignOfficerForVerification(int loanRequestId, int officerId)
        {
            try
            {
                var loanRequest = loanDbContext.LoanRequests.FirstOrDefault(lr => lr.LoanRequestId == loanRequestId);
                if (loanRequest != null)
                {
                    loanRequest.AssignedOfficerId = officerId;
                    loanDbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error assigning officer for verification: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public bool DeleteLoanRequest(int id)
        {
            try
            {
                var loanRequest = loanDbContext.LoanRequests.FirstOrDefault(lr => lr.LoanRequestId == id);
                if (loanRequest != null)
                {
                    loanDbContext.LoanRequests.Remove(loanRequest);
                    loanDbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error deleting loan request from database: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public List<LoanRequest> GetLoanRequests()
        {
            try
            {
                return loanDbContext.LoanRequests
                    .Include(lr => lr.Customer)
                    .Include(lr => lr.AssignedOfficer)
                    .ToList();
            }
            catch (Exception ex)
            {
                string errorMessage = "Error retrieving loan requests from database: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public LoanRequest? GetLoanRequestById(int id)
        {
            try
            {
                return loanDbContext.LoanRequests
                    .Include(lr => lr.Customer)
                    .Include(lr => lr.AssignedOfficer)
                    .FirstOrDefault(lr => lr.LoanRequestId == id);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error retrieving loan request from database: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public List<LoanRequest> GetLoanRequestsByCustomerId(int customerId)
        {
            try
            {
                return loanDbContext.LoanRequests
                    .Include(lr => lr.Customer)
                    .Include(lr => lr.AssignedOfficer)
                    .Where(lr => lr.CustomerId == customerId)
                    .ToList();
            }
            catch (Exception ex)
            {
                string errorMessage = "Error retrieving loan requests by customer: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public List<LoanRequest> GetLoanRequestsByOfficerId(int officerId)
        {
            try
            {
                return loanDbContext.LoanRequests
                    .Include(lr => lr.Customer)
                    .Include(lr => lr.AssignedOfficer)
                    .Where(lr => lr.AssignedOfficerId == officerId)
                    .ToList();
            }
            catch (Exception ex)
            {
                string errorMessage = "Error retrieving loan requests by officer: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public bool UpdateLoanRequest(int id, LoanRequest loanRequest)
        {
            try
            {
                var existingLoanRequest = loanDbContext.LoanRequests.FirstOrDefault(lr => lr.LoanRequestId == id);
                if (existingLoanRequest != null)
                {
                    existingLoanRequest.LoanType = loanRequest.LoanType;
                    existingLoanRequest.LoanAmount = loanRequest.LoanAmount;
                    existingLoanRequest.TenureMonths = loanRequest.TenureMonths;
                    existingLoanRequest.InterestRate = loanRequest.InterestRate;
                    existingLoanRequest.Purpose = loanRequest.Purpose;
                    existingLoanRequest.Status = loanRequest.Status;
                    loanDbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error updating loan request in database: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }
    }
}

