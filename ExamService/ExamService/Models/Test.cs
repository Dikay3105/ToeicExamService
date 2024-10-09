namespace ToeicWeb.Server.ExamService.Models
{
    public class Test
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Difficulty { get; set; }
        public int Duration { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
