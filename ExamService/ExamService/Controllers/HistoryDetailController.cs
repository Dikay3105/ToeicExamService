using Microsoft.AspNetCore.Mvc;
using ToeicWeb.ExamService.ExamService.Models;
using ToeicWeb.ExamService.ExamService.Repositories;

namespace ToeicWeb.ExamService.ExamService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryDetailController : ControllerBase
    {
        private readonly IHistoryDetailRepository _historyDetailRepository;

        public HistoryDetailController(IHistoryDetailRepository historyDetailRepository)
        {
            _historyDetailRepository = historyDetailRepository;
        }

        // GET: api/HistoryDetail
        [HttpGet]
        public async Task<IActionResult> GetHistoryDetails()
        {
            var historyDetails = await _historyDetailRepository.GetAllAsync();
            return Ok(new
            {
                EC = 0,
                EM = "Fetch all history details successfully",
                DT = historyDetails
            });
        }

        // GET: api/HistoryDetail/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHistoryDetail(int id)
        {
            var historyDetail = await _historyDetailRepository.GetByIdAsync(id);
            if (historyDetail == null)
            {
                return NotFound(new
                {
                    EC = 1,
                    EM = $"No history detail found with Id = {id}"
                });
            }

            return Ok(new
            {
                EC = 0,
                EM = "Fetch history detail successfully",
                DT = historyDetail
            });
        }

        // POST: api/HistoryDetail
        [HttpPost]
        public async Task<IActionResult> PostHistoryDetail([FromBody] HistoryDetail historyDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    EC = 1,
                    EM = "Invalid data"
                });
            }

            await _historyDetailRepository.AddAsync(historyDetail);
            return CreatedAtAction(nameof(GetHistoryDetail), new { id = historyDetail.Id }, new
            {
                EC = 0,
                EM = "Add history detail successfully",
                DT = historyDetail
            });
        }

        // PUT: api/HistoryDetail/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistoryDetail(int id, [FromBody] HistoryDetail historyDetail)
        {
            if (!ModelState.IsValid || id != historyDetail.Id)
            {
                return BadRequest(new
                {
                    EC = 1,
                    EM = "Invalid data or ID mismatch"
                });
            }

            var exists = await _historyDetailRepository.ExistsAsync(id);
            if (!exists)
            {
                return NotFound(new
                {
                    EC = 1,
                    EM = $"No history detail found with Id = {id}"
                });
            }

            await _historyDetailRepository.UpdateAsync(historyDetail);
            return Ok(new
            {
                EC = 0,
                EM = "Update history detail successfully",
                DT = historyDetail
            });
        }

        // DELETE: api/HistoryDetail/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistoryDetail(int id)
        {
            var exists = await _historyDetailRepository.ExistsAsync(id);
            if (!exists)
            {
                return NotFound(new
                {
                    EC = 1,
                    EM = $"No history detail found with Id = {id}"
                });
            }

            await _historyDetailRepository.DeleteAsync(id);
            return Ok(new
            {
                EC = 0,
                EM = "Delete history detail successfully"
            });
        }
    }
}
