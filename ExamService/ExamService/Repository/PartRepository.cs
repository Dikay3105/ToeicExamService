using Microsoft.EntityFrameworkCore;
using ToeicWeb.Server.ExamService.Data;
using ToeicWeb.Server.ExamService.Interfaces;
using ToeicWeb.Server.ExamService.Models;

namespace ToeicWeb.Server.ExamService.Repository
{
    public class PartRepository : IPartRepository
    {
        private readonly ExamDbContext _context;

        public PartRepository(ExamDbContext context)
        {
            _context = context;
        }
        public ICollection<Part> GetParts()
        {
            return _context.Parts.ToList();
        }
        public async Task<Part> GetPartById(int id)
        {
            return await _context.Parts.FindAsync(id);
        }
        public async Task<ICollection<Question>> GetQuestionOfPart(int id)
        {
            var parts = await _context.Questions
                                        .Where(a => a.PartID == id)
                                        .ToListAsync();

            return parts;
        }
    }
}
