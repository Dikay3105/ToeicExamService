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

        public async Task<IEnumerable<HistoryDto>> GetCombinedHistoriesAsync(int userId, int limit)
        {
            var combinedHistories = await (from hd in _context.HistoryDetails
                                           join h in _context.Histories on hd.HistoryID equals h.Id
                                           join t in _context.Tests on h.TestID equals t.Id
                                           join p in _context.Parts on hd.PartID equals p.Id
                                           where h.UserID == userId
                                           select new HistoryDto
                                           {
                                               Id = h.Id,
                                               UserID = h.UserID,
                                               TestID = h.TestID,
                                               Total_Listening = h.Total_Listening,
                                               Total_Reading = h.Total_Reading,
                                               StartTime = h.StartTime,
                                               EndTime = h.EndTime,
                                               UserName = u.Name, // Lấy tên người dùng từ bảng Users
                                               TestTitle = t.Title // Lấy tiêu đề bài kiểm tra từ bảng Tests
                                           })
                                            .Take(limit) // Giới hạn số lượng bản ghi trả về
                                            .ToListAsync();

            return combinedHistories;
        }


        public class HistoryDto
        {
            public int Id { get; set; }               // ID của History
            public int UserID { get; set; }           // ID của người dùng
            public int TestID { get; set; }           // ID của bài kiểm tra
            public int Total_Listening { get; set; }  // Tổng điểm nghe
            public int Total_Reading { get; set; }    // Tổng điểm đọc
            public DateTime StartTime { get; set; }   // Thời gian bắt đầu
            public DateTime EndTime { get; set; }     // Thời gian kết thúc
            public string UserName { get; set; }       // Tên người dùng (từ bảng Users)
            public string TestTitle { get; set; }      // Tiêu đề bài kiểm tra (từ bảng Tests)
        }

    }
}
