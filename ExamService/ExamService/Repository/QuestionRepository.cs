using Microsoft.EntityFrameworkCore;
using ToeicWeb.Server.ExamService.Data;
using ToeicWeb.Server.ExamService.Interfaces;
using ToeicWeb.Server.ExamService.Models;

namespace ToeicWeb.Server.ExamService.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ExamDbContext _context;

        public QuestionRepository(ExamDbContext context)
        {
            _context = context;
        }

        // Get all questions
        public ICollection<Question> GetQuestions()
        {
            return _context.Questions.ToList();
        }

        public async Task<Question> GetQuestionById(int id)
        {
            return await _context.Questions.FindAsync(id);
        }

        public async Task<ICollection<Answer>> GetAnswerOfQuestion(int id)
        {
            var answers = await _context.Answers
                                        .Where(a => a.QuestionID == id)
                                        .ToListAsync();

            return answers;
        }

        public async Task UpdateQuestion(Question question)
        {
            _context.Entry(question).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteQuestion(int id)
        {
            var question = await GetQuestionById(id);
            if (question != null)
            {
                _context.Questions.Remove(question);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddQuestion(Question question)
        {
            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();
        }

    }
}
