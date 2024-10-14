using Microsoft.AspNetCore.Mvc;
using ToeicWeb.ExamService.ExamService.Interfaces;
using ToeicWeb.ExamService.ExamService.Models;

namespace ToeicWeb.ExamService.ExamService.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class AnswerController : Controller
    {
        private readonly IAnswerRepository _answerRepository;

        public AnswerController(IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
        }

        // GET: /api/answer
        [HttpGet]
        public async Task<IActionResult> GetAllAnswers()
        {
            var answers = await _answerRepository.GetAllAnswers();
            return Ok(new
            {
                EC = 0,
                EM = "Get all answers success",
                DT = answers
            });
        }

        // GET: /api/answer/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnswerById(int id)
        {
            var answer = await _answerRepository.GetAnswerById(id);
            if (answer == null)
            {
                return NotFound(new
                {
                    EC = -1,
                    EM = "Answer not found",
                    DT = ""
                });
            }

            return Ok(new
            {
                EC = 0,
                EM = "Get answer by id success",
                DT = answer
            });
        }

        // POST: /api/answer
        [HttpPost]
        public async Task<IActionResult> CreateAnswer([FromBody] Answer newAnswer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    EC = -1,
                    EM = "Invalid data",
                    DT = ModelState.Values.SelectMany(v => v.Errors)
                });
            }

            await _answerRepository.CreateAnswer(newAnswer);

            return Ok(new
            {
                EC = 0,
                EM = "Answer created successfully",
                DT = newAnswer
            });
        }

        // PUT: /api/answer/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnswer(int id, [FromBody] Answer updatedAnswer)
        {
            var existingAnswer = await _answerRepository.GetAnswerById(id);
            if (existingAnswer == null)
            {
                return NotFound(new
                {
                    EC = -1,
                    EM = "Answer not found",
                    DT = ""
                });
            }

            existingAnswer.Text = updatedAnswer.Text;
            existingAnswer.IsCorrect = updatedAnswer.IsCorrect;

            await _answerRepository.UpdateAnswer(existingAnswer);

            return Ok(new
            {
                EC = 0,
                EM = "Answer updated successfully",
                DT = existingAnswer
            });
        }

        // DELETE: /api/answer/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnswer(int id)
        {
            var answer = await _answerRepository.GetAnswerById(id);
            if (answer == null)
            {
                return NotFound(new
                {
                    EC = -1,
                    EM = "Answer not found",
                    DT = ""
                });
            }

            await _answerRepository.DeleteAnswer(id);

            return Ok(new
            {
                EC = 0,
                EM = "Answer deleted successfully",
                DT = answer
            });
        }
    }
}
