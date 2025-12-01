using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LoanManagementAPI.Models;
using LoanManagementAPI.Repository;

namespace LoanManagementAPI.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _repository;

        public AdminController(IAdminRepository repository)
        {
            _repository = repository;
        }

        // ========== CUSTOMER MANAGEMENT ==========
        [HttpGet("customers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var customers = await _repository.GetAllCustomersAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("customers/{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            try
            {
                var customer = await _repository.GetCustomerByIdAsync(id);
                if (customer == null) return NotFound();
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }


        [HttpPost("customers/{id}/approve")]
        public async Task<IActionResult> ApproveCustomer(int id)
        {
            try
            {
                var approved = await _repository.ApproveCustomerAsync(id);
                if (!approved) return NotFound();
                return Ok("Customer approved");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost("customers/{id}/reject")]
        public async Task<IActionResult> RejectCustomer(int id)
        {
            try
            {
                var rejected = await _repository.RejectCustomerAsync(id);
                if (!rejected) return NotFound();
                return Ok("Customer rejected");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        // ========== LOAN OFFICER MANAGEMENT ==========
        [HttpGet("officers")]
        public async Task<IActionResult> GetAllOfficers()
        {
            try
            {
                var officers = await _repository.GetAllOfficersAsync();
                return Ok(officers);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("officers/{id}")]
        public async Task<IActionResult> GetOfficer(int id)
        {
            try
            {
                var officer = await _repository.GetOfficerByIdAsync(id);
                if (officer == null) return NotFound();
                return Ok(officer);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost("officers")]
        public async Task<IActionResult> AddOfficer([FromBody] LoanOfficer officer)
        {
            try
            {
                await _repository.AddOfficerAsync(officer);
                return Ok("Officer added");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }


        [HttpPost("officers/{id}/approve")]
        public async Task<IActionResult> ApproveOfficer(int id)
        {
            try
            {
                var approved = await _repository.ApproveOfficerAsync(id);
                if (!approved) return NotFound();
                return Ok("Officer approved");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost("officers/{id}/reject")]
        public async Task<IActionResult> RejectOfficer(int id)
        {
            try
            {
                var rejected = await _repository.RejectOfficerAsync(id);
                if (!rejected) return NotFound();
                return Ok("Officer rejected");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        // ========== LOAN REQUEST MANAGEMENT ==========
        [HttpGet("loan-requests")]
        public async Task<IActionResult> GetAllLoanRequests()
        {
            try
            {
                var requests = await _repository.GetAllLoanRequestsAsync();
                return Ok(requests);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("loan-requests/{id}")]
        public async Task<IActionResult> GetLoanRequest(int id)
        {
            try
            {
                var request = await _repository.GetLoanRequestByIdAsync(id);
                if (request == null) return NotFound();
                return Ok(request);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        // ========== VERIFICATION MANAGEMENT ==========
        [HttpGet("verifications/background")]
        public async Task<IActionResult> GetBackgroundVerifications()
        {
            try
            {
                var verifications = await _repository.GetBackgroundVerificationsAsync();
                return Ok(verifications);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("verifications/background/{id}")]
        public async Task<IActionResult> GetBackgroundVerification(int id)
        {
            try
            {
                var verification = await _repository.GetBackgroundVerificationByIdAsync(id);
                if (verification == null) return NotFound();
                return Ok(verification);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost("verifications/background")]
        public async Task<IActionResult> AddBackgroundVerification([FromBody] BackgroundVerification verification)
        {
            try
            {
                await _repository.AddBackgroundVerificationAsync(verification);
                return Ok("Background verification added");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }


        [HttpGet("verifications/loan")]
        public async Task<IActionResult> GetLoanVerifications()
        {
            try
            {
                var verifications = await _repository.GetLoanVerificationsAsync();
                return Ok(verifications);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("verifications/loan/{id}")]
        public async Task<IActionResult> GetLoanVerification(int id)
        {
            try
            {
                var verification = await _repository.GetLoanVerificationByIdAsync(id);
                if (verification == null) return NotFound();
                return Ok(verification);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost("verifications/loan")]
        public async Task<IActionResult> AddLoanVerification([FromBody] LoanVerification verification)
        {
            try
            {
                await _repository.AddLoanVerificationAsync(verification);
                return Ok("Loan verification added");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }


        // ========== FEEDBACK QUESTION MANAGEMENT ==========
        [HttpGet("feedback/questions")]
        public async Task<IActionResult> GetFeedbackQuestions()
        {
            try
            {
                var questions = await _repository.GetFeedbackQuestionsAsync();
                return Ok(questions);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("feedback/questions/{id}")]
        public async Task<IActionResult> GetFeedbackQuestion(int id)
        {
            try
            {
                var question = await _repository.GetFeedbackQuestionByIdAsync(id);
                if (question == null) return NotFound();
                return Ok(question);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost("feedback/questions")]
        public async Task<IActionResult> AddFeedbackQuestion([FromBody] FeedbackQuestion question)
        {
            try
            {
                await _repository.AddFeedbackQuestionAsync(question);
                return Ok("Question added");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }


        [HttpGet("feedback/customer")]
        public async Task<IActionResult> GetAllCustomerFeedbacks()
        {
            try
            {
                var feedbacks = await _repository.GetAllCustomerFeedbacksAsync();
                return Ok(feedbacks);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }


        // ========== HELP REPORT MANAGEMENT ==========
        [HttpGet("help-reports")]
        public async Task<IActionResult> GetHelpReports()
        {
            try
            {
                var reports = await _repository.GetHelpReportsAsync();
                return Ok(reports);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("help-reports/{id}")]
        public async Task<IActionResult> GetHelpReport(int id)
        {
            try
            {
                var report = await _repository.GetHelpReportByIdAsync(id);
                if (report == null) return NotFound();
                return Ok(report);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost("help-reports")]
        public async Task<IActionResult> AddHelpReport([FromBody] HelpReport report)
        {
            try
            {
                await _repository.AddHelpReportAsync(report);
                return Ok("Help report added");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
