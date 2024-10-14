using ToeicWeb.ExamService.ExamService.Models;

namespace ExamService.ExamService.Interfaces
{
    public interface IUserAnswerRepository
    {
        Task<List<UserAnswer>> GetUserAnswersByHistoryId(int historyId);
        Task AddUserAnswer(UserAnswer userAnswer);
        Task AddUserAnswers(List<UserAnswer> userAnswers);
    }
}
