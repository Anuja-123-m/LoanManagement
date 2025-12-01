using Microsoft.EntityFrameworkCore;
using LoanManagementAPI.Database;
using LoanManagementAPI.Models;

namespace LoanManagementAPI.Repository.FeedbackQuestionRepo
{
    public class FeedbackQuestionRepository : IFeedbackQuestionRepository
    {
        LoanDbContext loanDbContext;
        public FeedbackQuestionRepository(LoanDbContext loanDbContext)
        {
            this.loanDbContext = loanDbContext;
        }

        public void AddFeedbackQuestion(FeedbackQuestion question)
        {
            try
            {
                loanDbContext.FeedbackQuestions.Add(question);
                loanDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                string errorMessage = "Error adding feedback question to database: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public bool DeleteFeedbackQuestion(int id)
        {
            try
            {
                var question = loanDbContext.FeedbackQuestions.FirstOrDefault(q => q.QuestionId == id);
                if (question != null)
                {
                    loanDbContext.FeedbackQuestions.Remove(question);
                    loanDbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error deleting feedback question from database: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public List<FeedbackQuestion> GetFeedbackQuestions()
        {
            try
            {
                return loanDbContext.FeedbackQuestions.Where(q => q.IsActive).ToList();
            }
            catch (Exception ex)
            {
                string errorMessage = "Error retrieving feedback questions from database: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public FeedbackQuestion? GetFeedbackQuestionById(int id)
        {
            try
            {
                return loanDbContext.FeedbackQuestions.FirstOrDefault(q => q.QuestionId == id);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error retrieving feedback question from database: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public bool UpdateFeedbackQuestion(int id, FeedbackQuestion question)
        {
            try
            {
                var existingQuestion = loanDbContext.FeedbackQuestions.FirstOrDefault(q => q.QuestionId == id);
                if (existingQuestion != null)
                {
                    existingQuestion.QuestionText = question.QuestionText;
                    existingQuestion.QuestionType = question.QuestionType;
                    existingQuestion.IsActive = question.IsActive;
                    existingQuestion.UpdatedDate = DateTime.Now;
                    loanDbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error updating feedback question in database: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }
    }
}

