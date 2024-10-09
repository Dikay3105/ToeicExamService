using ToeicWeb.Server.ExamService.Models;

namespace ToeicWeb.Server.ExamService.Interfaces
{
    public interface IPartRepository
    {
        ICollection<Part> GetParts();
        Task<Part> GetPartById(int id);
        Task<ICollection<Question>> GetQuestionOfPart(int id);
    }
}
