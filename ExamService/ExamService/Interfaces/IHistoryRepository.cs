using ToeicWeb.ExamService.ExamService.Models;
using static ToeicWeb.ExamService.ExamService.Repository.HistoryRepository;

namespace ToeicWeb.ExamService.ExamService.Interfaces
{
    public interface IHistoryRepository
    {
        Task<IEnumerable<History>> GetAllHistoriesAsync();
        Task<History> GetHistoryByIdAsync(int id);
        Task AddHistoryAsync(History history);
        Task UpdateHistoryAsync(History history);
        Task DeleteHistoryAsync(int id);
        Task<History> GetHistoryByUserIdAndTestId(int historyId, int testId);
        Task<IEnumerable<HistoryDto>> GetCombinedHistoriesAsync(int userId, int hisId);
    }
}
