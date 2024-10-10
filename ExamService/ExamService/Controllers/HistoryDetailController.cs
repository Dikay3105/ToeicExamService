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
        public async Task<ActionResult<IEnumerable<HistoryDetail>>> GetHistoryDetails()
        {
            var historyDetails = await _historyDetailRepository.GetAllAsync();
            return Ok(historyDetails);
        }

        // GET: api/HistoryDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HistoryDetail>> GetHistoryDetail(int id)
        {
            var historyDetail = await _historyDetailRepository.GetByIdAsync(id);

            if (historyDetail == null)
            {
                return NotFound();
            }

            return Ok(historyDetail);
        }

        // POST: api/HistoryDetail
        [HttpPost]
        public async Task<ActionResult<HistoryDetail>> PostHistoryDetail(HistoryDetail historyDetail)
        {
            await _historyDetailRepository.AddAsync(historyDetail);
            return CreatedAtAction(nameof(GetHistoryDetail), new { id = historyDetail.Id }, historyDetail);
        }

        // PUT: api/HistoryDetail/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistoryDetail(int id, HistoryDetail historyDetail)
        {
            if (id != historyDetail.Id)
            {
                return BadRequest();
            }

            var exists = await _historyDetailRepository.ExistsAsync(id);
            if (!exists)
            {
                return NotFound();
            }

            await _historyDetailRepository.UpdateAsync(historyDetail);
            return NoContent();
        }

        // DELETE: api/HistoryDetail/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistoryDetail(int id)
        {
            var exists = await _historyDetailRepository.ExistsAsync(id);
            if (!exists)
            {
                return NotFound();
            }

            await _historyDetailRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
