using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LoanManagementAPI.Models;
using LoanManagementAPI.Repository.LoanVerificationRepo;

namespace LoanManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanVerificationController : ControllerBase
    {
        ILoanVerificationRepository _loanVerificationRepository;
        public LoanVerificationController(ILoanVerificationRepository loanVerificationRepository)
        {
            this._loanVerificationRepository = loanVerificationRepository;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            try
            {
                List<LoanVerification> verifications = _loanVerificationRepository.GetLoanVerifications();
                return Ok(verifications);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while retrieving loan verifications: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetById(int id)
        {
            try
            {
                LoanVerification? verification = _loanVerificationRepository.GetLoanVerificationById(id);
                if (verification != null)
                {
                    return Ok(verification);
                }
                return NotFound($"Loan Verification with id {id} does not exist");
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while retrieving loan verification: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpGet("loan-request/{loanRequestId}")]
        [Authorize]
        public IActionResult GetByLoanRequestId(int loanRequestId)
        {
            try
            {
                List<LoanVerification> verifications = _loanVerificationRepository.GetLoanVerificationsByLoanRequestId(loanRequestId);
                return Ok(verifications);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while retrieving loan verifications: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpGet("officer/{officerId}")]
        [Authorize]
        public IActionResult GetByOfficerId(int officerId)
        {
            try
            {
                List<LoanVerification> verifications = _loanVerificationRepository.GetLoanVerificationsByOfficerId(officerId);
                return Ok(verifications);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while retrieving loan verifications: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Add([FromBody] LoanVerification verification)
        {
            try
            {
                _loanVerificationRepository.AddLoanVerification(verification);
                return Ok("Loan Verification Added Successfully");
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while adding loan verification: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update(int id, [FromBody] LoanVerification verification)
        {
            try
            {
                bool isUpdated = _loanVerificationRepository.UpdateLoanVerification(id, verification);
                if (isUpdated)
                {
                    return Ok("Loan Verification Updated");
                }
                return BadRequest($"Loan Verification with id {id} does not exist");
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while updating loan verification: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool isDeleted = _loanVerificationRepository.DeleteLoanVerification(id);
                if (isDeleted)
                {
                    return Ok("Loan Verification Deleted");
                }
                return BadRequest($"Loan Verification with id {id} does not exist");
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while deleting loan verification: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpPost("{verificationId}/assign-officer/{officerId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult AssignOfficer(int verificationId, int officerId)
        {
            try
            {
                bool isAssigned = _loanVerificationRepository.AssignOfficer(verificationId, officerId);
                if (isAssigned)
                {
                    return Ok("Officer Assigned");
                }
                return BadRequest($"Loan Verification with id {verificationId} does not exist");
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while assigning officer: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }
    }
}

