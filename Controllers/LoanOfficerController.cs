using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LoanManagementAPI.Models;
using System.Security.Claims;
using LoanManagementAPI.Repository.LoanOfficerRepo;

namespace LoanManagementAPI.Controllers
{
    [Route("api/officer")]
    [ApiController]
    [Authorize(Roles = "LoanOfficer,Admin")]
    public class LoanOfficerController : ControllerBase
    {
        private readonly ILoanOfficerRepository _repository;

        public LoanOfficerController(ILoanOfficerRepository repository)
        {
            _repository = repository;
        }

        private string? GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);

        // ========== PROFILE MANAGEMENT ==========
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var userId = GetUserId();
                if (userId == null) return Unauthorized();

                var officer = await _repository.GetOfficerByUserIdAsync(userId);
                if (officer == null) return NotFound("Officer profile not found");
                return Ok(officer);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }


        // ========== ASSIGNED LOAN REQUESTS ==========
        [HttpGet("loan-requests")]
        public async Task<IActionResult> GetAssignedLoanRequests()
        {
            try
            {
                var userId = GetUserId();
                if (userId == null) return Unauthorized();

                var officer = await _repository.GetOfficerByUserIdAsync(userId);
                if (officer == null) return NotFound("Officer not found");

                var requests = await _repository.GetAssignedLoanRequestsAsync(officer.OfficerId);
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
                var userId = GetUserId();
                if (userId == null) return Unauthorized();

                var officer = await _repository.GetOfficerByUserIdAsync(userId);
                if (officer == null) return NotFound("Officer not found");

                var request = await _repository.GetAssignedLoanRequestByIdAsync(id, officer.OfficerId);
                if (request == null) return NotFound();
                return Ok(request);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        // ========== BACKGROUND VERIFICATIONS ==========
        [HttpGet("verifications/background")]
        public async Task<IActionResult> GetBackgroundVerifications()
        {
            try
            {
                var userId = GetUserId();
                if (userId == null) return Unauthorized();

                var officer = await _repository.GetOfficerByUserIdAsync(userId);
                if (officer == null) return NotFound("Officer not found");

                var verifications = await _repository.GetBackgroundVerificationsByOfficerIdAsync(officer.OfficerId);
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
                var userId = GetUserId();
                if (userId == null) return Unauthorized();

                var officer = await _repository.GetOfficerByUserIdAsync(userId);
                if (officer == null) return NotFound("Officer not found");

                var verification = await _repository.GetBackgroundVerificationByIdAsync(id, officer.OfficerId);
                if (verification == null) return NotFound();
                return Ok(verification);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }


        // ========== LOAN VERIFICATIONS ==========
        [HttpGet("verifications/loan")]
        public async Task<IActionResult> GetLoanVerifications()
        {
            try
            {
                var userId = GetUserId();
                if (userId == null) return Unauthorized();

                var officer = await _repository.GetOfficerByUserIdAsync(userId);
                if (officer == null) return NotFound("Officer not found");

                var verifications = await _repository.GetLoanVerificationsByOfficerIdAsync(officer.OfficerId);
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
                var userId = GetUserId();
                if (userId == null) return Unauthorized();

                var officer = await _repository.GetOfficerByUserIdAsync(userId);
                if (officer == null) return NotFound("Officer not found");

                var verification = await _repository.GetLoanVerificationByIdAsync(id, officer.OfficerId);
                if (verification == null) return NotFound();
                return Ok(verification);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

    }
}
