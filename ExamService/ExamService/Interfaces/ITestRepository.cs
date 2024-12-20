﻿using ToeicWeb.ExamService.ExamService.Models;

namespace ToeicWeb.ExamService.ExamService.Interfaces
{
    public interface ITestRepository
    {
        Task<IEnumerable<Test>> GetAllTestsAsync();
        Task<Test> GetTestByIdAsync(int id);
        Task AddTestAsync(Test test);
        Task UpdateTestAsync(Test test);
        Task DeleteTestAsync(int id);
        Task<ICollection<Part>> GetPartOfTest(int id);
    }
}
