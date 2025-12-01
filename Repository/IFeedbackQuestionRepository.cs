using LoanManagementAPI.Models;

namespace LoanManagementAPI.Repository.FeedbackQuestionRepo
{
    public interface IFeedbackQuestionRepository
    {
        List<FeedbackQuestion> GetFeedbackQuestions();
        FeedbackQuestion? GetFeedbackQuestionById(int id);
        void AddFeedbackQuestion(FeedbackQuestion question);
        bool UpdateFeedbackQuestion(int id, FeedbackQuestion question);
        bool DeleteFeedbackQuestion(int id);
    }
}

