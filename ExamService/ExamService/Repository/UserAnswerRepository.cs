using ExamService.ExamService.Interfaces;
using Microsoft.EntityFrameworkCore;
using ToeicWeb.ExamService.ExamService.Data;
using ToeicWeb.ExamService.ExamService.Models;

namespace ExamService.ExamService.Repository
{
    public class UserAnswerRepository : IUserAnswerRepository
    {
        private readonly ExamDbContext _context;

        public UserAnswerRepository(ExamDbContext context)
        {
            _context = context;
        }

        // Trả về danh sách UserAnswer theo HistoryID
        public async Task<List<UserAnswer>> GetUserAnswersByHistoryId(int historyId)
        {
            return await _context.UserAnswers
                .Where(ua => ua.HistoryID == historyId)
                .ToListAsync();
        }

        public async Task AddUserAnswer(UserAnswer userAnswer)
        {
            await _context.UserAnswers.AddAsync(userAnswer);
            await _context.SaveChangesAsync();
        }

        public async Task AddUserAnswers(List<UserAnswer> userAnswers)
        {
            await _context.UserAnswers.AddRangeAsync(userAnswers);
            await _context.SaveChangesAsync();
        }

    }
}
