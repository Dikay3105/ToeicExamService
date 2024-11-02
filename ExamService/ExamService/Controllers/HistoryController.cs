using Microsoft.AspNetCore.Mvc;
using ToeicWeb.ExamService.ExamService.Interfaces;
using ToeicWeb.ExamService.ExamService.Models;

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
            return Ok(new
            {
                EC = 0,
                EM = "Fetch all histories successfully",
                DT = histories
            });
        }

        // GET: api/History/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHistory(int id)
        {
            var history = await _historyRepository.GetHistoryByIdAsync(id);
            if (history == null)
            {
                return NotFound(new
                {
                    EC = 1,
                    EM = $"No history found with Id = {id}"
                });
            }

            return Ok(new
            {
                EC = 0,
                EM = "Fetch history successfully",
                DT = history
            });
        }

        // POST: api/History
        [HttpPost]
        public async Task<IActionResult> CreateHistory([FromBody] History history)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    EC = 1,
                    EM = "Invalid data"
                });
            }

            await _historyRepository.AddHistoryAsync(history);
            return Ok(new
            {
                EC = 0,
                EM = "Add history successfully",
                DT = history
            });
        }

        // PUT: api/History/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHistory([FromBody] History history)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    EC = 1,
                    EM = "Invalid data"
                });
            }

            var oldHistory = await _historyRepository.GetHistoryByIdAsync(history.Id);
            if (oldHistory == null)
            {
                return NotFound(new
                {
                    EC = 1,
                    EM = $"No data found with history Id = {history.Id}"
                });
            }

            await _historyRepository.UpdateHistoryAsync(history);
            return Ok(new
            {
                EC = 0,
                EM = "Update history successfully",
                DT = history
            });
        }

        // DELETE: api/History/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistory(int id)
        {
            var history = await _historyRepository.GetHistoryByIdAsync(id);
            if (history == null)
            {
                return NotFound(new
                {
                    EC = 1,
                    EM = $"No history found with Id = {id}"
                });
            }

            await _historyRepository.DeleteHistoryAsync(id);
            return Ok(new
            {
                EC = 0,
                EM = "Delete history successfully"
            });
        }
    }
}
