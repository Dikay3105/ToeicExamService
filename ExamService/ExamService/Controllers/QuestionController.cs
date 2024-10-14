using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using ToeicWeb.ExamService.ExamService.Data;
using ToeicWeb.ExamService.ExamService.Interfaces;
using ToeicWeb.ExamService.ExamService.Models;

namespace ToeicWeb.ExamService.ExamService.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class QuestionController : Controller
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ExamDbContext _context;

        private readonly Cloudinary _cloudinary;

        public QuestionController(IQuestionRepository questionRepository, ExamDbContext context, IConfiguration configuration)
        {
            _questionRepository = questionRepository;
            _context = context;

            // Cấu hình Cloudinary
            var cloudinaryAccount = new Account(
                configuration["Cloudinary:CloudName"],
                configuration["Cloudinary:ApiKey"],
                configuration["Cloudinary:ApiSecret"]
            );
            _cloudinary = new Cloudinary(cloudinaryAccount);
        }

        //Get all question
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Question>))]
        public IActionResult GetQuestions()
        {
            var question = _questionRepository.GetQuestions();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(new
            {
                EC = 0,
                EM = "Get all questions success",
                DT = question
            });
        }

        // Get question by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestionById(int id)
        {
            var question = await _questionRepository.GetQuestionById(id);
            if (question == null)
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
                EM = "Get question by id success",
                DT = question  // Data (can be an empty string or message)

            }); // Return the found user
        }

        // API endpoint để lấy câu trả lời của câu hỏi theo id
        [HttpGet("answer/{id}")]
        public async Task<IActionResult> GetAnswersOfQuestion(int id)
        {
            // Gọi phương thức trong repository để lấy câu trả lời
            var question = await _questionRepository.GetQuestionById(id);
            if (question == null)
            {
                return NotFound(new
                {
                    EC = -1,
                    EM = "No question found for the given question ID."
                });
            }

            var answers = await _questionRepository.GetAnswerOfQuestion(id);

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
                    question = question,
                    answers = answers
                }
            });
        }

        // Add question and img(audio) to Cloudinary
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Question))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddQuestion([FromForm] ModelAddQuestion model)
        {
            // Biến lưu URL hình ảnh và âm thanh nếu có
            string imageUrl = null;
            string audioUrl = null;
            string imageName = null;
            string audioName = null;

            // Nếu có hình ảnh, tải lên Cloudinary
            if (model.image != null && model.image.Length > 0)
            {
                var imageUploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(model.image.FileName, model.image.OpenReadStream()),
                };
                var imageUploadResult = await _cloudinary.UploadAsync(imageUploadParams);
                imageUrl = imageUploadResult.SecureUrl.AbsoluteUri; // Lưu trữ URL của hình ảnh
                imageName = model.image.FileName; // Lưu trữ name của hình ảnh
            }

            // Nếu có audio, tải lên Cloudinary
            if (model.audio != null && model.audio.Length > 0)
            {
                var audioUploadParams = new RawUploadParams()
                {
                    File = new FileDescription(model.audio.FileName, model.audio.OpenReadStream())
                };
                var audioUploadResult = await _cloudinary.UploadAsync(audioUploadParams);
                audioUrl = audioUploadResult.SecureUrl.AbsoluteUri; // Lưu trữ URL của âm thanh
                audioName = model.audio.FileName; // Lưu trữ name của âm thanh
            }

            // Tạo đối tượng câu hỏi mới từ model
            var question = new Question
            {
                PartID = model.PartID,
                Text = model.Text,
                ImagePath = imageUrl,  // URL hình ảnh nếu có
                AudioPath = audioUrl,  // URL âm thanh nếu có
                ImageName = imageName,
                AudioName = audioName,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,
                AnswerCounts = model.AnswerCounts
            };

            // Kiểm tra tính hợp lệ của model trước khi thêm vào cơ sở dữ liệu
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Thêm câu hỏi vào cơ sở dữ liệu qua repository
            await _questionRepository.AddQuestion(question);

            // Trả về kết quả thành công
            return CreatedAtAction(nameof(GetQuestionById), new { id = question.Id }, new
            {
                EC = 0,
                EM = "Add question success",
                DT = question
            });
        }


        // DELETE: api/question/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var existingQuestion = await _questionRepository.GetQuestionById(id);
            if (existingQuestion == null)
            {
                return NotFound(new { EC = -1, EM = "No question found" });
            }

            await _questionRepository.DeleteQuestion(id);
            return Ok(new
            {
                EC = 0,
                EM = "Delete question success"
            });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateQuestion(int id, [FromForm] ModelAddQuestion model)
        {
            // Kiểm tra tính hợp lệ của model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Lấy câu hỏi hiện tại từ cơ sở dữ liệu
            var existingQuestion = await _questionRepository.GetQuestionById(id);
            if (existingQuestion == null)
            {
                return NotFound(new { EC = -1, EM = "No question found" });
            }

            // Cờ để theo dõi có thay đổi nào cần update hay không
            bool hasUpdates = false;

            // Kiểm tra và cập nhật nếu thông tin PartID thay đổi
            if (existingQuestion.PartID != model.PartID)
            {
                existingQuestion.PartID = model.PartID;
                hasUpdates = true;
            }

            // Kiểm tra và cập nhật nếu Text thay đổi
            if (existingQuestion.Text != model.Text)
            {
                existingQuestion.Text = model.Text;
                hasUpdates = true;
            }

            // Kiểm tra và cập nhật nếu AnswerCounts thay đổi
            if (existingQuestion.AnswerCounts != model.AnswerCounts)
            {
                existingQuestion.AnswerCounts = model.AnswerCounts;
                hasUpdates = true;
            }

            // Kiểm tra hình ảnh mới có khác so với hình ảnh hiện tại không
            if (model.image != null && model.image.Length > 0)
            {
                var newImageFileName = Path.GetFileName(model.image.FileName); // Lấy tên tệp mới
                var currentImageFileName = existingQuestion.ImageName; // Lấy tên tệp hiện tại

                // Nếu hình ảnh mới khác với hình ảnh hiện tại, xóa hình ảnh cũ và tải hình ảnh mới lên
                if (!string.Equals(newImageFileName, currentImageFileName, StringComparison.OrdinalIgnoreCase))
                {
                    // Xóa tệp hình ảnh cũ nếu có
                    if (!string.IsNullOrEmpty(existingQuestion.ImagePath))
                    {
                        var publicId = existingQuestion.ImagePath.Split('/').Last().Split('.').First();
                        await _cloudinary.DeleteResourcesAsync(ResourceType.Image, publicId); // Xóa tệp cũ
                    }

                    // Tải lên hình ảnh mới
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(model.image.FileName, model.image.OpenReadStream()),
                    };
                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                    existingQuestion.ImagePath = uploadResult.SecureUrl.AbsoluteUri; // Lưu trữ URL của hình ảnh mới
                    hasUpdates = true; // Đánh dấu rằng đã có sự thay đổi
                }
            }

            // Kiểm tra âm thanh mới có khác so với âm thanh hiện tại không
            if (model.audio != null && model.audio.Length > 0)
            {
                var newAudioFileName = Path.GetFileName(model.audio.FileName); // Lấy tên tệp mới
                var currentAudioFileName = existingQuestion.AudioName; // Lấy tên tệp hiện tại

                // Nếu âm thanh mới khác với âm thanh hiện tại, xóa âm thanh cũ và tải âm thanh mới lên
                if (!string.Equals(newAudioFileName, currentAudioFileName, StringComparison.OrdinalIgnoreCase))
                {
                    // Xóa tệp âm thanh cũ nếu có
                    if (!string.IsNullOrEmpty(existingQuestion.AudioPath))
                    {
                        var publicId = existingQuestion.AudioPath.Split('/').Last(); // Lấy public ID từ URL
                        await _cloudinary.DestroyAsync(new DeletionParams(publicId) { ResourceType = ResourceType.Raw }); // Xóa tệp cũ
                    }

                    // Tải lên âm thanh mới
                    var uploadParams = new RawUploadParams()
                    {
                        File = new FileDescription(model.audio.FileName, model.audio.OpenReadStream())
                    };
                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                    existingQuestion.AudioPath = uploadResult.SecureUrl.AbsoluteUri; // Lưu trữ URL của âm thanh mới
                    hasUpdates = true; // Đánh dấu rằng đã có sự thay đổi
                }
            }

            // Nếu không có thay đổi nào, không thực hiện cập nhật
            if (!hasUpdates)
            {
                return Ok(new
                {
                    EC = 1,
                    EM = "No changes detected",
                    DT = existingQuestion
                });
            }

            // Cập nhật câu hỏi vào cơ sở dữ liệu qua repository
            await _questionRepository.UpdateQuestion(existingQuestion);

            return Ok(new
            {
                EC = 0,
                EM = "Update question success",
                DT = existingQuestion
            });
        }


    }



    public class ModelAddQuestion
    {
        public int PartID { get; set; }
        public string Text { get; set; }
        public IFormFile image { get; set; }
        public IFormFile audio { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int AnswerCounts { get; set; }
    }
}
