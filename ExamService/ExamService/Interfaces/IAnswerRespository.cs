using ToeicWeb.ExamService.ExamService.Models;

namespace ToeicWeb.ExamService.ExamService.Interfaces
{
    public interface IAnswerRepository
    {
        Task<IEnumerable<Answer>> GetAllAnswers();
        Task<Answer?> GetAnswerById(int id);
        Task CreateAnswer(Answer answer);
        Task UpdateAnswer(Answer answer);
        Task DeleteAnswer(int id);
        Task<bool> IsCorrectAnswer(int id);
    }
}
