using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using LoanManagementAPI.Models;
using System.Security.Claims;
using LoanManagementAPI.Repository.CustomerRepo;

namespace LoanManagementAPI.Controllers
{
    [Route("api/customer")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _repository;
        private readonly UserManager<User> _userManager;

        public CustomerController(ICustomerRepository repository, UserManager<User> userManager)
        {
            _repository = repository;
            _userManager = userManager;
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

                var customer = await _repository.GetCustomerByUserIdAsync(userId);
                if (customer == null) return NotFound("Customer profile not found");
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] CustomerRegistrationDto registrationDto)
        {
            try
            {
                // Check if user with this email already exists
                var existingUser = await _userManager.FindByEmailAsync(registrationDto.Email);
                if (existingUser != null)
                {
                    return BadRequest("A user with this email already exists.");
                }

                // Create User account
                var user = new User
                {
                    UserName = registrationDto.Email,
                    Email = registrationDto.Email,
                    FirstName = registrationDto.FirstName,
                    LastName = registrationDto.LastName,
                    Role = "Customer",
                    IsApproved = false,
                    CreatedDate = DateTime.Now
                };

                var result = await _userManager.CreateAsync(user, registrationDto.Password);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return BadRequest($"User creation failed: {errors}");
                }

                // Create Customer profile linked to the User
                var customer = new Customer
                {
                    FirstName = registrationDto.FirstName,
                    LastName = registrationDto.LastName,
                    Email = registrationDto.Email,
                    PhoneNumber = registrationDto.PhoneNumber,
                    Address = registrationDto.Address,
                    City = registrationDto.City,
                    State = registrationDto.State,
                    ZipCode = registrationDto.ZipCode,
                    Status = "Pending",
                    CreatedDate = DateTime.Now,
                    UserId = user.Id
                };

                await _repository.RegisterCustomerAsync(customer);
                return Ok("Customer registered successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        // ========== LOAN REQUEST MANAGEMENT ==========
        [HttpGet("loan-requests")]
        public async Task<IActionResult> GetMyLoanRequests()
        {
            try
            {
                var userId = GetUserId();
                if (userId == null) return Unauthorized();

                var customer = await _repository.GetCustomerByUserIdAsync(userId);
                if (customer == null) return NotFound("Customer not found");

                var requests = await _repository.GetLoanRequestsByCustomerIdAsync(customer.CustomerId);
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

                var customer = await _repository.GetCustomerByUserIdAsync(userId);
                if (customer == null) return NotFound("Customer not found");

                var request = await _repository.GetLoanRequestByIdAsync(id, customer.CustomerId);
                if (request == null) return NotFound();
                return Ok(request);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost("loan-requests")]
        public async Task<IActionResult> CreateLoanRequest([FromBody] LoanRequest loanRequest)
        {
            try
            {
                var userId = GetUserId();
                if (userId == null) return Unauthorized();

                var customer = await _repository.GetCustomerByUserIdAsync(userId);
                if (customer == null) return NotFound("Customer not found");

                loanRequest.CustomerId = customer.CustomerId;
                await _repository.CreateLoanRequestAsync(loanRequest);
                return Ok("Loan request created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

    }
}
