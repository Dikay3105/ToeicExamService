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

        public PartController(IPartRepository partRepository, ExamDbContext context)
        {
            _partRepository = partRepository;
            _context = context;
        }

        //Get all question
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

        // API endpoint để part của test
        [HttpGet("question/{id}")]
        public async Task<IActionResult> GetAnswersOfQuestion(int id)
        {
            // Gọi phương thức trong repository để lấy câu trả lời
            var question = await _partRepository.GetPartById(id);
            if (question == null)
            {
                return NotFound(new
                {
                    EC = -1,
                    EM = "No question found for the given question ID."
                });
            }

            var answers = await _partRepository.GetQuestionOfPart(id);

            // Kiểm tra nếu không có câu trả lời nào
            if (answers == null || !answers.Any())
            {
                return NotFound(new
                {
                    EC = -1,
                    EM = "No answers found for the given question ID."
                });
            }

            // Trả về danh sách câu trả lời
            return Ok(new
            {
                EC = 0,
                EM = "Get answer of questionId=" + id + " success",
                DT = new
                {
                    part = question,
                    question = answers
                }
            });
        }

    }
}
