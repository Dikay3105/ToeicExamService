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
    }
}
