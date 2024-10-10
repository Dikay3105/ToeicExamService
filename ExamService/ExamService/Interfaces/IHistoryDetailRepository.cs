using ToeicWeb.ExamService.ExamService.Models;

namespace ToeicWeb.ExamService.ExamService.Repositories
{
    public interface IHistoryDetailRepository
    {
        Task<IEnumerable<HistoryDetail>> GetAllAsync();
        Task<HistoryDetail> GetByIdAsync(int id);
        Task AddAsync(HistoryDetail historyDetail);
        Task UpdateAsync(HistoryDetail historyDetail);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
