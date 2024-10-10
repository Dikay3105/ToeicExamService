using Microsoft.EntityFrameworkCore;
using ToeicWeb.ExamService.ExamService.Data;
using ToeicWeb.ExamService.ExamService.Models;

namespace ToeicWeb.ExamService.ExamService.Repositories
{
    public class HistoryDetailRepository : IHistoryDetailRepository
    {
        private readonly ExamDbContext _context;

        public HistoryDetailRepository(ExamDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HistoryDetail>> GetAllAsync()
        {
            return await _context.HistoryDetails.ToListAsync();
        }

        public async Task<HistoryDetail> GetByIdAsync(int id)
        {
            return await _context.HistoryDetails.FindAsync(id);
        }

        public async Task AddAsync(HistoryDetail historyDetail)
        {
            _context.HistoryDetails.Add(historyDetail);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(HistoryDetail historyDetail)
        {
            _context.Entry(historyDetail).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var historyDetail = await _context.HistoryDetails.FindAsync(id);
            if (historyDetail != null)
            {
                _context.HistoryDetails.Remove(historyDetail);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.HistoryDetails.AnyAsync(e => e.Id == id);
        }
    }
}
