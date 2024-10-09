using ToeicWeb.Server.ExamService.Models;

namespace ToeicWeb.Server.ExamService.Interfaces
{
    public interface ITestRepository
    {
        Task<IEnumerable<Test>> GetAllTestsAsync();
        Task<Test> GetTestByIdAsync(int id);
        Task AddTestAsync(Test test);
        Task UpdateTestAsync(Test test);
        Task DeleteTestAsync(int id);
        Task<ICollection<Part>> GetPartOfTest(int id);
    }
}
