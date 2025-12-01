using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LoanManagementAPI.Models;
using LoanManagementAPI.Repository.LoanRequestRepo;

namespace LoanManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanRequestController : ControllerBase
    {
        ILoanRequestRepository _loanRequestRepository;
        public LoanRequestController(ILoanRequestRepository loanRequestRepository)
        {
            this._loanRequestRepository = loanRequestRepository;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            try
            {
                List<LoanRequest> loanRequests = _loanRequestRepository.GetLoanRequests();
                return Ok(loanRequests);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while retrieving loan requests: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetById(int id)
        {
            try
            {
                LoanRequest? loanRequest = _loanRequestRepository.GetLoanRequestById(id);
                if (loanRequest != null)
                {
                    return Ok(loanRequest);
                }
                return NotFound($"Loan Request with id {id} does not exist");
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while retrieving loan request: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpGet("customer/{customerId}")]
        [Authorize]
        public IActionResult GetByCustomerId(int customerId)
        {
            try
            {
                List<LoanRequest> loanRequests = _loanRequestRepository.GetLoanRequestsByCustomerId(customerId);
                return Ok(loanRequests);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while retrieving loan requests: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpGet("officer/{officerId}")]
        [Authorize]
        public IActionResult GetByOfficerId(int officerId)
        {
            try
            {
                List<LoanRequest> loanRequests = _loanRequestRepository.GetLoanRequestsByOfficerId(officerId);
                return Ok(loanRequests);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while retrieving loan requests: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add([FromBody] LoanRequest loanRequest)
        {
            try
            {
                _loanRequestRepository.AddLoanRequest(loanRequest);
                return Ok("Loan Request Added Successfully");
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while adding loan request: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update(int id, [FromBody] LoanRequest loanRequest)
        {
            try
            {
                bool isLoanRequestUpdated = _loanRequestRepository.UpdateLoanRequest(id, loanRequest);
                if (isLoanRequestUpdated)
                {
                    return Ok("Loan Request Updated");
                }
                return BadRequest($"Loan Request with id {id} does not exist");
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while updating loan request: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool isLoanRequestDeleted = _loanRequestRepository.DeleteLoanRequest(id);
                if (isLoanRequestDeleted)
                {
                    return Ok("Loan Request Deleted");
                }
                return BadRequest($"Loan Request with id {id} does not exist");
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while deleting loan request: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpPost("{loanRequestId}/assign-officer/{officerId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult AssignOfficerForVerification(int loanRequestId, int officerId)
        {
            try
            {
                bool isAssigned = _loanRequestRepository.AssignOfficerForVerification(loanRequestId, officerId);
                if (isAssigned)
                {
                    return Ok("Officer Assigned for Verification");
                }
                return BadRequest($"Loan Request with id {loanRequestId} does not exist");
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while assigning officer: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpPost("{loanRequestId}/assign-officer-loan-verification/{officerId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult AssignOfficerForLoanVerification(int loanRequestId, int officerId)
        {
            try
            {
                bool isAssigned = _loanRequestRepository.AssignOfficerForLoanVerification(loanRequestId, officerId);
                if (isAssigned)
                {
                    return Ok("Officer Assigned for Loan Verification");
                }
                return BadRequest($"Loan Request with id {loanRequestId} does not exist");
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while assigning officer: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }
    }
}

