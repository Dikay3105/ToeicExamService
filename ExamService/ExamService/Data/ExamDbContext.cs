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
        }
    }
}
