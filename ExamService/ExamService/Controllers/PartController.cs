using ExamService.ExamService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ToeicWeb.ExamService.ExamService.Data;
using ToeicWeb.ExamService.ExamService.Interfaces;
using ToeicWeb.ExamService.ExamService.Models;
using ToeicWeb.ExamService.ExamService.Repositories;

namespace ToeicWeb.ExamService.ExamService.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class PartController : Controller
    {
        private readonly IPartRepository _partRepository;
        private readonly ExamDbContext _context;
        private readonly IAnswerRepository _answerRepository;
        private readonly IHistoryDetailRepository _historyDetailRepository;
        private readonly IUserAnswerRepository _userAnswerRepository;
        private readonly IQuestionRepository _questionRepository;

        public PartController(IPartRepository partRepository,
            ExamDbContext context,
            IAnswerRepository answerRepository,
            IHistoryDetailRepository historyDetailRepository,
            IUserAnswerRepository userAnswerRepository,
            IQuestionRepository questionRepository
            )
        {
            _partRepository = partRepository;
            _context = context;
            _answerRepository = answerRepository;
            _historyDetailRepository = historyDetailRepository;
            _userAnswerRepository = userAnswerRepository;
            _questionRepository = questionRepository;
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

        [HttpGet("question/{id}")]
        public async Task<IActionResult> GetAnswersOfQuestion(int id)
        {
            // Lấy thông tin phần từ repository
            var part = await _partRepository.GetPartById(id);
            if (part == null)
            {
                return NotFound(new
                {
                    EC = -1,
                    EM = "No part found for the given part ID."
                });
            }

            // Lấy danh sách câu hỏi của phần
            var questions = await _partRepository.GetQuestionOfPart(id);

            // Kiểm tra nếu không có câu hỏi nào
            if (questions == null || !questions.Any())
            {
                return NotFound(new
                {
                    EC = -1,
                    EM = "No questions found for the given part ID."
                });
            }

            // Tạo danh sách chứa câu hỏi và câu trả lời
            var listQuesAns = new List<object>();

            foreach (var ques in questions)
            {
                // Lấy danh sách câu trả lời cho câu hỏi hiện tại
                var answers = await _questionRepository.GetAnswerOfQuestion(ques.Id);

                // Nếu không có câu trả lời, bỏ qua câu hỏi này
                if (answers == null || !answers.Any())
                {
                    return NotFound(new
                    {
                        EC = -1,
                        EM = $"No answers found for question ID {ques.Id}."
                    });
                }

                // Thêm câu hỏi và câu trả lời vào danh sách
                listQuesAns.Add(new
                {
                    question = ques,
                    answers = answers
                });
            }

            // Trả về kết quả với cấu trúc mong muốn
            return Ok(new
            {
                EC = 0,
                EM = "Get questions and answers success",
                DT = new
                {
                    part = new
                    {
                        id = part.Id,
                        name = part.Name,
                        questions = listQuesAns
                    }
                }
            });
        }


        [HttpPost("submit")]
        public async Task<IActionResult> SubmitPart([FromBody] PartSubmissionDto request)
        {
            // Kiểm tra Part có tồn tại hay không
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

            // Khởi tạo danh sách lưu trữ các UserAnswer
            var userAnswersToAdd = new List<UserAnswer>();

            // Kiểm tra từng đáp án của người dùng và đếm số câu trả lời đúng
            foreach (var userAnswer in request.Answers)
            {
                var isCorrect = await _answerRepository.IsCorrectAnswer(userAnswer.UserAnswerId);
                if (isCorrect)
                {
                    correctAnswers++;
                }

                // Thêm từng câu trả lời của người dùng vào danh sách
                var newUserAnswer = new UserAnswer
                {
                    UserID = request.UserId,
                    QuestionID = userAnswer.QuestionId,
                    SelectedAnswerID = userAnswer.UserAnswerId,
                    HistoryID = request.HistoryId,
                    IsCorrect = isCorrect
                };

                userAnswersToAdd.Add(newUserAnswer);
            }

            // Thêm tất cả UserAnswers vào cơ sở dữ liệu trong một lần
            await _userAnswerRepository.AddUserAnswers(userAnswersToAdd);

            // Thêm thông tin chi tiết vào HistoryDetail
            var historyDetail = new HistoryDetail
            {
                PartID = request.PartId,
                HistoryID = request.HistoryId,
                TotalCorrect = correctAnswers,
                TotalQuestion = totalQuestions
            };

            await _historyDetailRepository.AddAsync(historyDetail);

            // Trả về kết quả
            return Ok(new
            {
                EC = 0,
                EM = "Submit part success",
                DT = new
                {
                    TotalQuestions = totalQuestions,
                    CorrectAnswers = correctAnswers
                }
            });
        }




        //DTO dữ liệu đầu vào chấm điểm theo part
        public class PartSubmissionDto
        {
            public int PartId { get; set; }
            public int HistoryId { get; set; }
            public int UserId { get; set; }
            public List<AnswerDto> Answers { get; set; }
        }

        // DTO cho câu trả lời của người dùng
        public class AnswerDto
        {
            public int QuestionId { get; set; }
            public int UserAnswerId { get; set; }
        }
    }

}
