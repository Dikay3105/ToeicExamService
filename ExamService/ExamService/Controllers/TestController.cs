using Microsoft.AspNetCore.Mvc;
using ToeicWeb.ExamService.ExamService.Interfaces;
using ToeicWeb.ExamService.ExamService.Models;

namespace ToeicWeb.ExamService.ExamService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestRepository _testRepository;
        private readonly IPartRepository _partRepository;

        public TestController(ITestRepository testRepository, IPartRepository partRepository)
        {
            _testRepository = testRepository;
            _partRepository = partRepository;
        }

        // GET: api/Test
        [HttpGet]
        public async Task<IActionResult> GetAllTests()
        {
            var tests = await _testRepository.GetAllTestsAsync();
            return Ok(new
            {
                EC = 0,
                EM = "Get all tests success",
                DT = tests
            });
        }

        // GET: api/Test/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTest(int id)
        {
            var test = await _testRepository.GetTestByIdAsync(id);
            if (test == null)
            {
                return NotFound(new
                {
                    EC = -1,
                    EM = "Get test fail",
                    DT = test
                });
            }

            return Ok(new
            {
                EC = 0,
                EM = "Get test success",
                DT = test
            });
        }

        // POST: api/Test
        [HttpPost]
        public async Task<IActionResult> CreateTest([FromBody] Test test)
        {
            if (ModelState.IsValid)
            {
                await _testRepository.AddTestAsync(test);
                return BadRequest(new
                {
                    EC = 0,
                    EM = "Add test success",
                    DT = test
                });

            }

            return BadRequest(new
            {
                EC = -1,
                EM = "Add test fail",
                DT = ModelState
            });
        }

        // PUT: api/Test/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTest(int id, [FromBody] Test test)
        {
            if (id != test.Id)
            {
                return BadRequest(new
                {
                    EC = -1,
                    EM = "Update test fail",
                    DT = ModelState
                });
            }

            await _testRepository.UpdateTestAsync(test);
            return Ok(new
            {
                EC = 0,
                EM = "Update test success",
                DT = test
            });
        }

        // DELETE: api/Test/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTest(int id)
        {
            await _testRepository.DeleteTestAsync(id);
            return Ok(new
            {
                EC = 0,
                EM = "Del test success",
            });
        }

        // API endpoint để lấy part cuả test theo id
        [HttpGet("part/{id}")]
        public async Task<IActionResult> GetAnswersOfQuestion(int id)
        {
            // Gọi phương thức trong repository để lấy câu trả lời
            var test = await _testRepository.GetTestByIdAsync(id);
            if (test == null)
            {
                return NotFound(new
                {
                    EC = -1,
                    EM = "No test found for the given test ID."
                });
            }

            var parts = await _testRepository.GetPartOfTest(id);

            // Kiểm tra nếu không có câu trả lời nào
            if (parts == null || !parts.Any())
            {
                return NotFound(new
                {
                    EC = -1,
                    EM = "No parts found for the given test ID."
                });
            }

            // Trả về danh sách câu trả lời
            return Ok(new
            {
                EC = 0,
                EM = "Get part of testID=" + id + " success",
                DT = new
                {
                    test = test,
                    parts = parts
                }
            });
        }
    }
}
