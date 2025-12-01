using Microsoft.EntityFrameworkCore;
using LoanManagementAPI.Database;
using LoanManagementAPI.Models;

namespace LoanManagementAPI.Repository.BackgroundVerificationRepo
{
    public class BackgroundVerificationRepository : IBackgroundVerificationRepository
    {
        LoanDbContext loanDbContext;
        public BackgroundVerificationRepository(LoanDbContext loanDbContext)
        {
            this.loanDbContext = loanDbContext;
        }

        public void AddBackgroundVerification(BackgroundVerification verification)
        {
            try
            {
                loanDbContext.BackgroundVerifications.Add(verification);
                loanDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                string errorMessage = "Error adding background verification to database: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public bool AssignOfficer(int verificationId, int officerId)
        {
            try
            {
                var verification = loanDbContext.BackgroundVerifications.FirstOrDefault(v => v.VerificationId == verificationId);
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

        public bool DeleteBackgroundVerification(int id)
        {
            try
            {
                var verification = loanDbContext.BackgroundVerifications.FirstOrDefault(v => v.VerificationId == id);
                if (verification != null)
                {
                    loanDbContext.BackgroundVerifications.Remove(verification);
                    loanDbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error deleting background verification from database: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public List<BackgroundVerification> GetBackgroundVerifications()
        {
            try
            {
                return loanDbContext.BackgroundVerifications
                    .Include(v => v.LoanRequest)
                    .Include(v => v.AssignedOfficer)
                    .ToList();
            }
            catch (Exception ex)
            {
                string errorMessage = "Error retrieving background verifications from database: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public BackgroundVerification? GetBackgroundVerificationById(int id)
        {
            try
            {
                return loanDbContext.BackgroundVerifications
                    .Include(v => v.LoanRequest)
                    .Include(v => v.AssignedOfficer)
                    .FirstOrDefault(v => v.VerificationId == id);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error retrieving background verification from database: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public List<BackgroundVerification> GetBackgroundVerificationsByLoanRequestId(int loanRequestId)
        {
            try
            {
                return loanDbContext.BackgroundVerifications
                    .Include(v => v.LoanRequest)
                    .Include(v => v.AssignedOfficer)
                    .Where(v => v.LoanRequestId == loanRequestId)
                    .ToList();
            }
            catch (Exception ex)
            {
                string errorMessage = "Error retrieving background verifications by loan request: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public List<BackgroundVerification> GetBackgroundVerificationsByOfficerId(int officerId)
        {
            try
            {
                return loanDbContext.BackgroundVerifications
                    .Include(v => v.LoanRequest)
                    .Include(v => v.AssignedOfficer)
                    .Where(v => v.AssignedOfficerId == officerId)
                    .ToList();
            }
            catch (Exception ex)
            {
                string errorMessage = "Error retrieving background verifications by officer: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public bool UpdateBackgroundVerification(int id, BackgroundVerification verification)
        {
            try
            {
                var existingVerification = loanDbContext.BackgroundVerifications.FirstOrDefault(v => v.VerificationId == id);
                if (existingVerification != null)
                {
                    existingVerification.VerificationType = verification.VerificationType;
                    existingVerification.Details = verification.Details;
                    existingVerification.Status = verification.Status;
                    if (verification.Status == "Completed" || verification.Status == "Failed")
                    {
                        existingVerification.CompletedDate = DateTime.Now;
                    }
                    loanDbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error updating background verification in database: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }
    }
}

