using LoanManagementAPI.Models;

namespace LoanManagementAPI.Repository.CustomerFeedbackRepo
{
    public interface ICustomerFeedbackRepository
    {
        List<CustomerFeedback> GetCustomerFeedbacks();
        CustomerFeedback? GetCustomerFeedbackById(int id);
        List<CustomerFeedback> GetCustomerFeedbacksByCustomerId(int customerId);
        void AddCustomerFeedback(CustomerFeedback feedback);
        bool UpdateCustomerFeedback(int id, CustomerFeedback feedback);
        bool DeleteCustomerFeedback(int id);
    }
}

