using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LoanManagementAPI.Models;
using LoanManagementAPI.Repository.BackgroundVerificationRepo;

namespace LoanManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackgroundVerificationController : ControllerBase
    {
        IBackgroundVerificationRepository _backgroundVerificationRepository;
        public BackgroundVerificationController(IBackgroundVerificationRepository backgroundVerificationRepository)
        {
            this._backgroundVerificationRepository = backgroundVerificationRepository;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            try
            {
                List<BackgroundVerification> verifications = _backgroundVerificationRepository.GetBackgroundVerifications();
                return Ok(verifications);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while retrieving background verifications: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetById(int id)
        {
            try
            {
                BackgroundVerification? verification = _backgroundVerificationRepository.GetBackgroundVerificationById(id);
                if (verification != null)
                {
                    return Ok(verification);
                }
                return NotFound($"Background Verification with id {id} does not exist");
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while retrieving background verification: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpGet("loan-request/{loanRequestId}")]
        [Authorize]
        public IActionResult GetByLoanRequestId(int loanRequestId)
        {
            try
            {
                List<BackgroundVerification> verifications = _backgroundVerificationRepository.GetBackgroundVerificationsByLoanRequestId(loanRequestId);
                return Ok(verifications);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while retrieving background verifications: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpGet("officer/{officerId}")]
        [Authorize]
        public IActionResult GetByOfficerId(int officerId)
        {
            try
            {
                List<BackgroundVerification> verifications = _backgroundVerificationRepository.GetBackgroundVerificationsByOfficerId(officerId);
                return Ok(verifications);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while retrieving background verifications: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Add([FromBody] BackgroundVerification verification)
        {
            try
            {
                _backgroundVerificationRepository.AddBackgroundVerification(verification);
                return Ok("Background Verification Added Successfully");
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while adding background verification: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update(int id, [FromBody] BackgroundVerification verification)
        {
            try
            {
                bool isUpdated = _backgroundVerificationRepository.UpdateBackgroundVerification(id, verification);
                if (isUpdated)
                {
                    return Ok("Background Verification Updated");
                }
                return BadRequest($"Background Verification with id {id} does not exist");
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while updating background verification: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool isDeleted = _backgroundVerificationRepository.DeleteBackgroundVerification(id);
                if (isDeleted)
                {
                    return Ok("Background Verification Deleted");
                }
                return BadRequest($"Background Verification with id {id} does not exist");
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while deleting background verification: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpPost("{verificationId}/assign-officer/{officerId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult AssignOfficer(int verificationId, int officerId)
        {
            try
            {
                bool isAssigned = _backgroundVerificationRepository.AssignOfficer(verificationId, officerId);
                if (isAssigned)
                {
                    return Ok("Officer Assigned");
                }
                return BadRequest($"Background Verification with id {verificationId} does not exist");
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while assigning officer: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }
    }
}

