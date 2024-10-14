using Microsoft.EntityFrameworkCore;
using ToeicWeb.ExamService.ExamService.Data;
using ToeicWeb.ExamService.ExamService.Interfaces;
using ToeicWeb.ExamService.ExamService.Models;

namespace ToeicWeb.ExamService.ExamService.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly ExamDbContext _context;

        public AnswerRepository(ExamDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Answer>> GetAllAnswers()
        {
            return await _context.Answers.ToListAsync();
        }

        public async Task<Answer?> GetAnswerById(int id)
        {
            return await _context.Answers.FindAsync(id);
        }

        public async Task CreateAnswer(Answer answer)
        {
            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAnswer(Answer answer)
        {
            _context.Answers.Update(answer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAnswer(int id)
        {
            var answer = await GetAnswerById(id);
            if (answer != null)
            {
                _context.Answers.Remove(answer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsCorrectAnswer(int id)
        {
            var answer = await GetAnswerById(id);
            if (answer == null)
                return false;  // Trả về null nếu không tìm thấy đáp án

            return answer.IsCorrect;
        }
    }
}
