using Microsoft.EntityFrameworkCore;
using ToeicWeb.ExamService.ExamService.Data;
using ToeicWeb.ExamService.ExamService.Interfaces;
using ToeicWeb.ExamService.ExamService.Models;

namespace ToeicWeb.ExamService.ExamService.Repository
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
        public async Task UpdatePartAsync(Part part)
        {
            // Đánh dấu đối tượng Part là đã thay đổi
            _context.Parts.Update(part);

            // Lưu các thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();
        }

        // Add part
        public async Task AddPartAsync(Part part)
        {
            await _context.Parts.AddAsync(part);
            await _context.SaveChangesAsync();
        }

    }
}
