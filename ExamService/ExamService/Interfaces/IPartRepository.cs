using ToeicWeb.ExamService.ExamService.Models;

namespace ToeicWeb.ExamService.ExamService.Interfaces
{
    public interface IPartRepository
    {
        ICollection<Part> GetParts();
        Task<Part> GetPartById(int id);
        Task<ICollection<Question>> GetQuestionOfPart(int id);
        Task UpdatePartAsync(Part part);
        Task AddPartAsync(Part part);
    }
}
