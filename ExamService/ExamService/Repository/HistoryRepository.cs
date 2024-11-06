using Microsoft.EntityFrameworkCore;
using ToeicWeb.ExamService.ExamService.Data;
using ToeicWeb.ExamService.ExamService.Interfaces;
using ToeicWeb.ExamService.ExamService.Models;

namespace ToeicWeb.ExamService.ExamService.Repository
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly ExamDbContext _context;

        public HistoryRepository(ExamDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<History>> GetAllHistoriesAsync()
        {
            return await _context.Histories.ToListAsync();
        }

        public async Task<History> GetHistoryByIdAsync(int id)
        {
            return await _context.Histories.FindAsync(id);
        }
        public async Task<History> GetHistoryByUserIdAndTestId(int historyId, int testId)
        {
            return await _context.Histories
                .Where(h => h.Id == historyId && h.TestID == testId)
                .FirstOrDefaultAsync();
        }

        public async Task AddHistoryAsync(History history)
        {
            _context.Histories.Add(history);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateHistoryAsync(History history)
        {
            _context.Histories.Update(history);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteHistoryAsync(int id)
        {
            var history = await _context.Histories.FindAsync(id);
            if (history != null)
            {
                _context.Histories.Remove(history);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<HistoryDto>> GetCombinedHistoriesAsync(int userId, int hisId)
        {
            var combinedHistories = await (from hd in _context.HistoryDetails
                                           join h in _context.Histories on hd.HistoryID equals h.Id
                                           join t in _context.Tests on h.TestID equals t.Id
                                           join p in _context.Parts on hd.PartID equals p.Id
                                           where h.UserID == userId
                                             && h.Id == hisId
                                           select new HistoryDto
                                           {
                                               PardId = hd.PartID,
                                               PartName = p.Name,
                                               Total_Question = hd.TotalQuestion,
                                               Total_Correct = hd.TotalCorrect,
                                           })
                                            .ToListAsync();

            return combinedHistories;
        }


        public class HistoryDto
        {
            public int PardId { get; set; }           // ID của người dùng
            public string PartName { get; set; }           // ID của bài kiểm tra
            public int Total_Question { get; set; }  // Tổng điểm nghe
            public int Total_Correct { get; set; }    // Tổng điểm đọc
        }

    }
}
