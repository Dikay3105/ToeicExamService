using ExamService.ExamService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ToeicWeb.ExamService.ExamService.Data;

namespace ToeicWeb.ExamService.ExamService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAnswerController : ControllerBase
    {
        private readonly ExamDbContext _context;
        private readonly Interfaces.IHistoryRepository _historyRepository;
        private readonly IUserAnswerRepository _userAnswerRepository;

        public UserAnswerController(ExamDbContext context, Interfaces.IHistoryRepository historyRepository, IUserAnswerRepository userAnswerRepository)
        {
            _context = context;
            _historyRepository = historyRepository;
            _userAnswerRepository = userAnswerRepository;
        }

        // API lấy danh sách UserAnswer theo HistoryID
        [HttpGet("{historyId}")]
        public async Task<IActionResult> GetUserAnswersByHistoryId(int historyId)
        {
            var history = await _historyRepository.GetHistoryByIdAsync(historyId);
            if (history == null)
            {
                return NotFound(new
                {
                    EC = -1,
                    EM = "No history found for this history ID",
                    DT = ""
                });
            }

            var userAnswers = await _userAnswerRepository.GetUserAnswersByHistoryId(historyId);

            if (userAnswers.Count == 0 || userAnswers == null)
            {
                return NotFound(new
                {
                    EC = -1,
                    EM = "No answers found for this history ID",
                    DT = ""
                });
            }

            return Ok(new
            {
                EC = 0,
                EM = "User answers retrieved successfully",
                DT = userAnswers
            });
        }
    }
}
