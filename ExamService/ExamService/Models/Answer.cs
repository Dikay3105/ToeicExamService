namespace ToeicWeb.Server.ExamService.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionID { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}
