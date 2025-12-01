using Microsoft.EntityFrameworkCore;
using LoanManagementAPI.Database;
using LoanManagementAPI.Models;

namespace LoanManagementAPI.Repository.LoanVerificationRepo
{
    public class LoanVerificationRepository : ILoanVerificationRepository
    {
        LoanDbContext loanDbContext;
        public LoanVerificationRepository(LoanDbContext loanDbContext)
        {
            this.loanDbContext = loanDbContext;
        }

        public void AddLoanVerification(LoanVerification verification)
        {
            try
            {
                loanDbContext.LoanVerifications.Add(verification);
                loanDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                string errorMessage = "Error adding loan verification to database: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public bool AssignOfficer(int verificationId, int officerId)
        {
            try
            {
                var verification = loanDbContext.LoanVerifications.FirstOrDefault(v => v.VerificationId == verificationId);
                if (verification != null)
                {
                    verification.AssignedOfficerId = officerId;
                    loanDbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error assigning officer: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public bool DeleteLoanVerification(int id)
        {
            try
            {
                var verification = loanDbContext.LoanVerifications.FirstOrDefault(v => v.VerificationId == id);
                if (verification != null)
                {
                    loanDbContext.LoanVerifications.Remove(verification);
                    loanDbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error deleting loan verification from database: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public List<LoanVerification> GetLoanVerifications()
        {
            try
            {
                return loanDbContext.LoanVerifications
                    .Include(v => v.LoanRequest)
                    .Include(v => v.AssignedOfficer)
                    .ToList();
            }
            catch (Exception ex)
            {
                string errorMessage = "Error retrieving loan verifications from database: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public LoanVerification? GetLoanVerificationById(int id)
        {
            try
            {
                return loanDbContext.LoanVerifications
                    .Include(v => v.LoanRequest)
                    .Include(v => v.AssignedOfficer)
                    .FirstOrDefault(v => v.VerificationId == id);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error retrieving loan verification from database: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public List<LoanVerification> GetLoanVerificationsByLoanRequestId(int loanRequestId)
        {
            try
            {
                return loanDbContext.LoanVerifications
                    .Include(v => v.LoanRequest)
                    .Include(v => v.AssignedOfficer)
                    .Where(v => v.LoanRequestId == loanRequestId)
                    .ToList();
            }
            catch (Exception ex)
            {
                string errorMessage = "Error retrieving loan verifications by loan request: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public List<LoanVerification> GetLoanVerificationsByOfficerId(int officerId)
        {
            try
            {
                return loanDbContext.LoanVerifications
                    .Include(v => v.LoanRequest)
                    .Include(v => v.AssignedOfficer)
                    .Where(v => v.AssignedOfficerId == officerId)
                    .ToList();
            }
            catch (Exception ex)
            {
                string errorMessage = "Error retrieving loan verifications by officer: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public bool UpdateLoanVerification(int id, LoanVerification verification)
        {
            try
            {
                var existingVerification = loanDbContext.LoanVerifications.FirstOrDefault(v => v.VerificationId == id);
                if (existingVerification != null)
                {
                    existingVerification.VerificationDetails = verification.VerificationDetails;
                    existingVerification.Status = verification.Status;
                    existingVerification.Remarks = verification.Remarks;
                    if (verification.Status == "Verified" || verification.Status == "Rejected")
                    {
                        existingVerification.VerifiedDate = DateTime.Now;
                    }
                    loanDbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error updating loan verification in database: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }
    }
}

