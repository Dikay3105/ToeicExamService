namespace ToeicWeb.ExamService.ExamService.Models
{
    public class History
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public int TestID { get; set; }
        public int Total_Listening { get; set; }
        public int Total_Reading { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
