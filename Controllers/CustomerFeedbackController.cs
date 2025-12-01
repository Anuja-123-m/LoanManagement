using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LoanManagementAPI.Models;
using LoanManagementAPI.Repository.CustomerFeedbackRepo;

namespace LoanManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerFeedbackController : ControllerBase
    {
        ICustomerFeedbackRepository _customerFeedbackRepository;
        public CustomerFeedbackController(ICustomerFeedbackRepository customerFeedbackRepository)
        {
            this._customerFeedbackRepository = customerFeedbackRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Get()
        {
            try
            {
                List<CustomerFeedback> feedbacks = _customerFeedbackRepository.GetCustomerFeedbacks();
                return Ok(feedbacks);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while retrieving customer feedbacks: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetById(int id)
        {
            try
            {
                CustomerFeedback? feedback = _customerFeedbackRepository.GetCustomerFeedbackById(id);
                if (feedback != null)
                {
                    return Ok(feedback);
                }
                return NotFound($"Customer Feedback with id {id} does not exist");
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while retrieving customer feedback: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpGet("customer/{customerId}")]
        [Authorize]
        public IActionResult GetByCustomerId(int customerId)
        {
            try
            {
                List<CustomerFeedback> feedbacks = _customerFeedbackRepository.GetCustomerFeedbacksByCustomerId(customerId);
                return Ok(feedbacks);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while retrieving customer feedbacks: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add([FromBody] CustomerFeedback feedback)
        {
            try
            {
                _customerFeedbackRepository.AddCustomerFeedback(feedback);
                return Ok("Customer Feedback Added Successfully");
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while adding customer feedback: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update(int id, [FromBody] CustomerFeedback feedback)
        {
            try
            {
                bool isUpdated = _customerFeedbackRepository.UpdateCustomerFeedback(id, feedback);
                if (isUpdated)
                {
                    return Ok("Customer Feedback Updated");
                }
                return BadRequest($"Customer Feedback with id {id} does not exist");
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while updating customer feedback: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool isDeleted = _customerFeedbackRepository.DeleteCustomerFeedback(id);
                if (isDeleted)
                {
                    return Ok("Customer Feedback Deleted");
                }
                return BadRequest($"Customer Feedback with id {id} does not exist");
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while deleting customer feedback: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }
    }
}

