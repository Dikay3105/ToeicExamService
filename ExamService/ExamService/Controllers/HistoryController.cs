using Microsoft.AspNetCore.Mvc;
using ToeicWeb.ExamService.ExamService.Interfaces;
using ToeicWeb.ExamService.ExamService.Models;
using static ToeicWeb.ExamService.ExamService.Repository.HistoryRepository;

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

        // API để lấy lịch sử kết hợp theo userId và hisId
        [HttpGet("PartOfHistory/{userId}/{hisId}")]
        public async Task<IActionResult> GetCombinedHistories(int userId, int hisId)
        {
            try
            {
                IEnumerable<HistoryDto> histories = await _historyRepository.GetCombinedHistoriesAsync(userId, hisId);

                if (histories == null || !histories.Any())
                {
                    return Ok(new
                    {
                        EC = -1,
                        EM = "Không tìm thấy lịch sử."
                    });
                }

                return Ok(new
                {
                    EC = 0,
                    EM = "Success",
                    DT = histories
                });
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khi có exception
                return Ok(new
                {
                    EC = -1,
                    EM = "Đã xảy ra lỗi khi lấy dữ liệu."
                });
            }
        }

        //// API để lấy lịch sử kết hợp theo userId và hisId
        //[HttpGet("PartOfHistory/{userId}/{hisId}")]
        //public async Task<IActionResult> GetCombinedHistories2(int userId, int hisId)
        //{
        //    try
        //    {
        //        IEnumerable<HistoryDto> histories = await _historyRepository.GetCombinedHistoriesWithQuestionsAndAnswersAsync(userId, hisId);

        //        if (histories == null || !histories.Any())
        //        {
        //            return Ok(new
        //            {
        //                EC = -1,
        //                EM = "Không tìm thấy lịch sử."
        //            });
        //        }

        //        return Ok(new
        //        {
        //            EC = 0,
        //            EM = "Success",
        //            DT = histories.Select(history => new
        //            {
        //                history.Id,
        //                history.Date,
        //                history.Score,
        //                Questions = history.Questions.Select(question => new
        //                {
        //                    question.QuestionId,
        //                    question.Content,
        //                    Answers = question.Answers.Select(answer => new
        //                    {
        //                        answer.AnswerId,
        //                        answer.Content,
        //                        answer.IsCorrect // Nếu muốn biết đáp án đúng hay không (tùy chọn)
        //                    }).ToList()
        //                }).ToList()
        //            })
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Xử lý lỗi khi có exception
        //        return Ok(new
        //        {
        //            EC = -1,
        //            EM = "Đã xảy ra lỗi khi lấy dữ liệu."
        //        });
        //    }
        //}

    }
}
