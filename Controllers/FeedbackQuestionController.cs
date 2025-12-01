using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LoanManagementAPI.Models;
using LoanManagementAPI.Repository.FeedbackQuestionRepo;

namespace LoanManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackQuestionController : ControllerBase
    {
        IFeedbackQuestionRepository _feedbackQuestionRepository;
        public FeedbackQuestionController(IFeedbackQuestionRepository feedbackQuestionRepository)
        {
            this._feedbackQuestionRepository = feedbackQuestionRepository;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            try
            {
                List<FeedbackQuestion> questions = _feedbackQuestionRepository.GetFeedbackQuestions();
                return Ok(questions);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while retrieving feedback questions: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetById(int id)
        {
            try
            {
                FeedbackQuestion? question = _feedbackQuestionRepository.GetFeedbackQuestionById(id);
                if (question != null)
                {
                    return Ok(question);
                }
                return NotFound($"Feedback Question with id {id} does not exist");
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while retrieving feedback question: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Add([FromBody] FeedbackQuestion question)
        {
            try
            {
                _feedbackQuestionRepository.AddFeedbackQuestion(question);
                return Ok("Feedback Question Added Successfully");
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while adding feedback question: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id, [FromBody] FeedbackQuestion question)
        {
            try
            {
                bool isUpdated = _feedbackQuestionRepository.UpdateFeedbackQuestion(id, question);
                if (isUpdated)
                {
                    return Ok("Feedback Question Updated");
                }
                return BadRequest($"Feedback Question with id {id} does not exist");
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while updating feedback question: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool isDeleted = _feedbackQuestionRepository.DeleteFeedbackQuestion(id);
                if (isDeleted)
                {
                    return Ok("Feedback Question Deleted");
                }
                return BadRequest($"Feedback Question with id {id} does not exist");
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while deleting feedback question: " + ex.Message;
                return BadRequest(errorMessage);
            }
        }
    }
}

