using Microsoft.AspNetCore.Mvc;
using ToeicWeb.Server.ExamService.Interfaces;
using ToeicWeb.Server.ExamService.Models;

namespace ExamService.ExamService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryRepository _historyRepository;

        public HistoryController(IHistoryRepository historyRepository)
        {
            _historyRepository = historyRepository;
        }

        // GET: api/History
        [HttpGet]
        public async Task<IActionResult> GetAllHistories()
        {
            var histories = await _historyRepository.GetAllHistoriesAsync();
            return Ok(histories);
        }

        // GET: api/History/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHistory(int id)
        {
            var history = await _historyRepository.GetHistoryByIdAsync(id);
            if (history == null)
            {
                return NotFound();
            }

            return Ok(history);
        }

        // POST: api/History
        [HttpPost]
        public async Task<IActionResult> CreateHistory([FromBody] History history)
        {
            if (ModelState.IsValid)
            {
                await _historyRepository.AddHistoryAsync(history);
                return CreatedAtAction(nameof(GetHistory), new { id = history.Id }, history);
            }

            return BadRequest(ModelState);
        }

        // PUT: api/History/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHistory(int id, [FromBody] History history)
        {
            if (id != history.Id)
            {
                return BadRequest();
            }

            await _historyRepository.UpdateHistoryAsync(history);
            return NoContent();
        }

        // DELETE: api/History/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistory(int id)
        {
            await _historyRepository.DeleteHistoryAsync(id);
            return NoContent();
        }
    }
}
