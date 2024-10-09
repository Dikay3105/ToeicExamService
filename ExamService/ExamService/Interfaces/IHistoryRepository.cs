using ToeicWeb.Server.ExamService.Models;

namespace ToeicWeb.Server.ExamService.Interfaces
{
    public interface IHistoryRepository
    {
        Task<IEnumerable<History>> GetAllHistoriesAsync();
        Task<History> GetHistoryByIdAsync(int id);
        Task AddHistoryAsync(History history);
        Task UpdateHistoryAsync(History history);
        Task DeleteHistoryAsync(int id);
    }
}
