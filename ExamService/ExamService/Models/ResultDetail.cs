namespace ToeicWeb.Server.ExamService.Models
{
    public class HistoryDetail
    {
        public int Id { get; set; }
        public int PartID { get; set; }
        public int HistoryID { get; set; }
        public int TotalQuestion { get; set; }
        public int TotalAnswer { get; set; }

    }
}
