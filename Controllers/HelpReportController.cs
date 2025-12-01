using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LoanManagementAPI.Models;
using LoanManagementAPI.Repository.HelpReportRepo;

namespace LoanManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelpReportController : ControllerBase
    {
        IHelpReportRepository _helpReportRepository;
        public HelpReportController(IHelpReportRepository helpReportRepository)
        {
            this._helpReportRepository = helpReportRepository;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            try
            {
                List<HelpReport> helpReports = _helpReportRepository.GetHelpReports();
                return Ok(helpReports);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while retrieving help reports: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetById(int id)
        {
            try
            {
                HelpReport? helpReport = _helpReportRepository.GetHelpReportById(id);
                if (helpReport != null)
                {
                    return Ok(helpReport);
                }
                return NotFound($"Help Report with id {id} does not exist");
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while retrieving help report: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add([FromBody] HelpReport helpReport)
        {
            try
            {
                _helpReportRepository.AddHelpReport(helpReport);
                return Ok("Help Report Added Successfully");
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while adding help report: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id, [FromBody] HelpReport helpReport)
        {
            try
            {
                bool isUpdated = _helpReportRepository.UpdateHelpReport(id, helpReport);
                if (isUpdated)
                {
                    return Ok("Help Report Updated");
                }
                return BadRequest($"Help Report with id {id} does not exist");
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while updating help report: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool isDeleted = _helpReportRepository.DeleteHelpReport(id);
                if (isDeleted)
                {
                    return Ok("Help Report Deleted");
                }
                return BadRequest($"Help Report with id {id} does not exist");
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while deleting help report: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }
    }
}

