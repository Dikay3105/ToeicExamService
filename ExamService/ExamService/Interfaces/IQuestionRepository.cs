using ToeicWeb.Server.ExamService.Models;

namespace ToeicWeb.Server.ExamService.Interfaces
{
    public interface IQuestionRepository
    {
        ICollection<Question> GetQuestions();
        Task<Question> GetQuestionById(int id);
        Task<ICollection<Answer>> GetAnswerOfQuestion(int id);
        Task AddQuestion(Question question);
        Task UpdateQuestion(Question question);
        Task DeleteQuestion(int id);
    }
}
