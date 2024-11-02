using Microsoft.EntityFrameworkCore;
using ToeicWeb.ExamService.ExamService.Models;

namespace ToeicWeb.ExamService.ExamService.Data
{
    public class ExamDbContext : DbContext
    {
        public ExamDbContext(DbContextOptions<ExamDbContext> options)
            : base(options)
        {
        }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<HistoryDetail> HistoryDetails { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Explicitly map entity to table names to avoid EF pluralization issues
            modelBuilder.Entity<History>().ToTable("history").HasKey(h => h.Id);
            modelBuilder.Entity<HistoryDetail>().ToTable("historydetail").HasKey(hd => hd.Id);

            // Relationships
            modelBuilder.Entity<Answer>()
                .HasOne<Question>()
                .WithMany()
                .HasForeignKey(a => a.QuestionID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<History>()
                .HasOne<Test>()
                .WithMany()
                .HasForeignKey(h => h.TestID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Part>()
                .HasOne<Test>()
                .WithMany()
                .HasForeignKey(p => p.TestID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Question>()
                .HasOne<Part>()
                .WithMany()
                .HasForeignKey(q => q.PartID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HistoryDetail>()
                .HasOne<Part>()
                .WithMany()
                .HasForeignKey(hd => hd.PartID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HistoryDetail>()
                .HasOne<History>()
                .WithMany()
                .HasForeignKey(hd => hd.HistoryID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserAnswer>()
                .HasOne<Question>()
                .WithMany()
                .HasForeignKey(ua => ua.QuestionID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserAnswer>()
                .HasOne<Answer>()
                .WithMany()
                .HasForeignKey(ua => ua.SelectedAnswerID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserAnswer>()
                .HasOne<History>()
                .WithMany()
                .HasForeignKey(ua => ua.HistoryID)
                .OnDelete(DeleteBehavior.Cascade);

            // Dữ liệu cho bảng Test
            modelBuilder.Entity<Test>().HasData(
                new Test { Id = 1, Name = "Test 1", Description = "Mô tả bài kiểm tra 1", Difficulty = "Easy", Duration = 60, CreatedAt = DateTime.Now },
                new Test { Id = 2, Name = "Test 2", Description = "Mô tả bài kiểm tra 2", Difficulty = "Medium", Duration = 75, CreatedAt = DateTime.Now },
                new Test { Id = 3, Name = "Test 3", Description = "Mô tả bài kiểm tra 3", Difficulty = "Hard", Duration = 90, CreatedAt = DateTime.Now }
            );

            // Dữ liệu cho bảng Answers
            modelBuilder.Entity<Answer>().HasData(
                new Answer { Id = 1, QuestionID = 1, Text = "He is eating", IsCorrect = false },
                new Answer { Id = 2, QuestionID = 1, Text = "He is talking on the phone", IsCorrect = true },
                new Answer { Id = 3, QuestionID = 1, Text = "He is walking", IsCorrect = false },
                new Answer { Id = 4, QuestionID = 1, Text = "He is reading", IsCorrect = false },
                new Answer { Id = 5, QuestionID = 2, Text = "In a conference room", IsCorrect = true },
                new Answer { Id = 6, QuestionID = 2, Text = "In a cafe", IsCorrect = false },
                new Answer { Id = 7, QuestionID = 2, Text = "In a park", IsCorrect = false },
                new Answer { Id = 8, QuestionID = 2, Text = "At the airport", IsCorrect = false }
            );

            // Dữ liệu cho bảng Parts
            modelBuilder.Entity<Part>().HasData(
                new Part { Id = 1, Name = "Part 1", Number = 1, Description = "Listening Comprehension", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, TestID = 1 },
                new Part { Id = 2, Name = "Part 2", Number = 2, Description = "Reading Comprehension", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, TestID = 1 },
                new Part { Id = 3, Name = "Part 1", Number = 1, Description = "Listening Comprehension", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, TestID = 2 },
                new Part { Id = 4, Name = "Part 2", Number = 2, Description = "Reading Comprehension", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, TestID = 2 }
            );

            // Dữ liệu cho bảng Questions
            modelBuilder.Entity<Question>().HasData(
                new Question { Id = 1, PartID = 1, Text = "What is he doing?", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, AnswerCounts = 4 },
                new Question { Id = 2, PartID = 1, Text = "Where is he going?", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, AnswerCounts = 4 },
                new Question { Id = 3, PartID = 2, Text = "What does the sign say?", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, AnswerCounts = 4 },
                new Question { Id = 4, PartID = 2, Text = "What is the main idea of the passage?", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, AnswerCounts = 4 }
            );

            // Dữ liệu cho bảng UserAnswers
            modelBuilder.Entity<UserAnswer>().HasData(
                new UserAnswer { Id = 1, UserID = 1, QuestionID = 1, SelectedAnswerID = 2, HistoryID = 1, IsCorrect = true },
                new UserAnswer { Id = 2, UserID = 1, QuestionID = 2, SelectedAnswerID = 5, HistoryID = 1, IsCorrect = true },
                new UserAnswer { Id = 3, UserID = 2, QuestionID = 3, SelectedAnswerID = 11, HistoryID = 2, IsCorrect = true },
                new UserAnswer { Id = 4, UserID = 2, QuestionID = 4, SelectedAnswerID = 15, HistoryID = 2, IsCorrect = true }
            );

            // Dữ liệu cho bảng Histories
            modelBuilder.Entity<History>().HasData(
                new History { Id = 1, UserID = 1, TestID = 1, Total_Listening = 75, Total_Reading = 80, StartTime = DateTime.Now.AddHours(-2), EndTime = DateTime.Now },
                new History { Id = 2, UserID = 2, TestID = 2, Total_Listening = 85, Total_Reading = 90, StartTime = DateTime.Now.AddHours(-2), EndTime = DateTime.Now }
            );

            // Dữ liệu cho bảng HistoryDetails
            modelBuilder.Entity<HistoryDetail>().HasData(
                new HistoryDetail { Id = 1, PartID = 1, HistoryID = 1, TotalQuestion = 10, TotalCorrect = 8 },
                new HistoryDetail { Id = 2, PartID = 2, HistoryID = 1, TotalQuestion = 10, TotalCorrect = 7 },
                new HistoryDetail { Id = 3, PartID = 3, HistoryID = 2, TotalQuestion = 10, TotalCorrect = 9 },
                new HistoryDetail { Id = 4, PartID = 4, HistoryID = 2, TotalQuestion = 10, TotalCorrect = 6 }
            );
        }


    }
}
