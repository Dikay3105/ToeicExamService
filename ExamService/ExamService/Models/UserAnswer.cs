namespace ToeicWeb.Server.ExamService.Models
{
    public class UserAnswer
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public int QuestionID { get; set; }
        public int SelectedAnswerID { get; set; }
        public int HistoryID { get; set; }
        public bool IsCorrect { get; set; }
    }
}
