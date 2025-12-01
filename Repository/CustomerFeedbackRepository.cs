using Microsoft.EntityFrameworkCore;
using LoanManagementAPI.Database;
using LoanManagementAPI.Models;

namespace LoanManagementAPI.Repository.CustomerFeedbackRepo
{
    public class CustomerFeedbackRepository : ICustomerFeedbackRepository
    {
        LoanDbContext loanDbContext;
        public CustomerFeedbackRepository(LoanDbContext loanDbContext)
        {
            this.loanDbContext = loanDbContext;
        }

        public void AddCustomerFeedback(CustomerFeedback feedback)
        {
            try
            {
                loanDbContext.CustomerFeedbacks.Add(feedback);
                loanDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                string errorMessage = "Error adding customer feedback to database: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public bool DeleteCustomerFeedback(int id)
        {
            try
            {
                var feedback = loanDbContext.CustomerFeedbacks.FirstOrDefault(f => f.FeedbackId == id);
                if (feedback != null)
                {
                    loanDbContext.CustomerFeedbacks.Remove(feedback);
                    loanDbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error deleting customer feedback from database: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public List<CustomerFeedback> GetCustomerFeedbacks()
        {
            try
            {
                return loanDbContext.CustomerFeedbacks
                    .Include(f => f.Customer)
                    .Include(f => f.FeedbackQuestion)
                    .ToList();
            }
            catch (Exception ex)
            {
                string errorMessage = "Error retrieving customer feedbacks from database: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public CustomerFeedback? GetCustomerFeedbackById(int id)
        {
            try
            {
                return loanDbContext.CustomerFeedbacks
                    .Include(f => f.Customer)
                    .Include(f => f.FeedbackQuestion)
                    .FirstOrDefault(f => f.FeedbackId == id);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error retrieving customer feedback from database: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public List<CustomerFeedback> GetCustomerFeedbacksByCustomerId(int customerId)
        {
            try
            {
                return loanDbContext.CustomerFeedbacks
                    .Include(f => f.Customer)
                    .Include(f => f.FeedbackQuestion)
                    .Where(f => f.CustomerId == customerId)
                    .ToList();
            }
            catch (Exception ex)
            {
                string errorMessage = "Error retrieving customer feedbacks by customer: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public bool UpdateCustomerFeedback(int id, CustomerFeedback feedback)
        {
            try
            {
                var existingFeedback = loanDbContext.CustomerFeedbacks.FirstOrDefault(f => f.FeedbackId == id);
                if (existingFeedback != null)
                {
                    existingFeedback.FeedbackText = feedback.FeedbackText;
                    existingFeedback.Rating = feedback.Rating;
                    loanDbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error updating customer feedback in database: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }
    }
}

