using Microsoft.AspNetCore.Mvc;
using ToeicWeb.ExamService.ExamService.Data;
using ToeicWeb.ExamService.ExamService.Interfaces;
using ToeicWeb.ExamService.ExamService.Models;

namespace ToeicWeb.ExamService.ExamService.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class PartController : Controller
    {
        private readonly IPartRepository _partRepository;
        private readonly ExamDbContext _context;
        private readonly IAnswerRepository _answerRepository;

        public PartController(IPartRepository partRepository, ExamDbContext context, IAnswerRepository answerRepository)
        {
            _partRepository = partRepository;
            _context = context;
            _answerRepository = answerRepository;
        }

        //Get all part
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Question>))]
        public IActionResult GetParts()
        {
            var part = _partRepository.GetParts();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(new
            {
                EC = 0,
                EM = "Get all parts success",
                DT = part
            });
        }

        // Get part by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Part>> GetPartById(int id)
        {
            var part = await _partRepository.GetPartById(id);
            if (part == null)
            {
                return NotFound(new
                {
                    EC = -1,  // Error Code
                    EM = "No data found",
                    DT = "",   // Data (can be an empty string or message)

                }); // Return 404 if user not found
            }

            return Ok(new
            {
                EC = 0,  // Error Code
                EM = "Get part by id success",
                DT = part  // Data (can be an empty string or message)

            }); // Return the found user
        }

        // API endpoint để get question của part theo id
        [HttpGet("question/{id}")]
        public async Task<IActionResult> GetAnswersOfQuestion(int id)
        {
            // Gọi phương thức trong repository để lấy câu trả lời
            var part = await _partRepository.GetPartById(id);
            if (part == null)
            {
                return NotFound(new
                {
                    EC = -1,
                    EM = "No part found for the given part ID."
                });
            }

            var questions = await _partRepository.GetQuestionOfPart(id);

            // Kiểm tra nếu không có câu trả lời nào
            if (questions == null || !questions.Any())
            {
                return NotFound(new
                {
                    EC = -1,
                    EM = "No questions found for the given part ID."
                });
            }

            // Trả về danh sách câu trả lời
            return Ok(new
            {
                EC = 0,
                EM = "Get question of partID=" + id + " success",
                DT = new
                {
                    part,
                    questions
                }
            });
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitPart([FromBody] PartSubmissionDto request)
        {
            var part = await _partRepository.GetPartById(request.PartId);
            if (part == null)
            {
                return NotFound(new
                {
                    EC = -1,
                    EM = "Part not found",
                    DT = ""
                });
            }

            int totalQuestions = request.Answers.Count;
            int correctAnswers = 0;

            // Kiểm tra từng đáp án của người dùng
            foreach (var userAnswer in request.Answers)
            {
                var correct = await _answerRepository.IsCorrectAnswer(userAnswer.UserAnswerId);
                if (correct == true)
                {
                    correctAnswers++;
                }
            }

            return Ok(new
            {
                EC = 0,
                EM = "Submit part success",
                DT = new
                {
                    TotalQuestions = totalQuestions,
                    CorrectAnswers = correctAnswers,
                }
            });
        }


    }

    //DTO dữ liệu đầu vào chấm điểm theo part
    public class PartSubmissionDto
    {
        public int PartId { get; set; }
        public List<AnswerDto> Answers { get; set; }
    }

    // DTO cho câu trả lời của người dùng
    public class AnswerDto
    {
        public int QuestionId { get; set; }
        public int UserAnswerId { get; set; }
    }
}
