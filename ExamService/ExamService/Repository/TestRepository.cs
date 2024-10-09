using Microsoft.EntityFrameworkCore;
using ToeicWeb.Server.ExamService.Data;
using ToeicWeb.Server.ExamService.Interfaces;
using ToeicWeb.Server.ExamService.Models;

namespace ToeicWeb.Server.ExamService.Repository
{
    public class TestRepository : ITestRepository
    {
        private readonly ExamDbContext _context;

        public TestRepository(ExamDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Test>> GetAllTestsAsync()
        {
            return await _context.Tests.ToListAsync();
        }

        public async Task<Test> GetTestByIdAsync(int id)
        {
            return await _context.Tests.FindAsync(id);
        }

        public async Task AddTestAsync(Test test)
        {
            _context.Tests.Add(test);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTestAsync(Test test)
        {
            _context.Tests.Update(test);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTestAsync(int id)
        {
            var test = await _context.Tests.FindAsync(id);
            if (test != null)
            {
                _context.Tests.Remove(test);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ICollection<Part>> GetPartOfTest(int id)
        {
            var parts = await _context.Parts
                                        .Where(a => a.TestID == id)
                                        .ToListAsync();

            return parts;
        }
    }
}
