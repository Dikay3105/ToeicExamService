namespace ToeicWeb.Server.ExamService.Models
{
    public class Question
    {
        public int Id { get; set; }
        public int PartID { get; set; }
        public string Text { get; set; }
        public string? ImagePath { get; set; }
        public string? AudioPath { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int AnswerCounts { get; set; }
    }
}
