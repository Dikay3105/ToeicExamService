namespace ToeicWeb.Server.ExamService.Services
{
    public class AuthServiceClient
    {
        private readonly HttpClient _httpClient;

        public AuthServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Ví dụ: Lấy thông tin người dùng từ AuthService
        //public async Task<UserDto> GetUserInfoAsync(string token)
        //{
        //     Gửi yêu cầu HTTP tới AuthService với JWT token
        //    var request = new HttpRequestMessage(HttpMethod.Get, "/api/user/info");
        //    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        //    var response = await _httpClient.SendAsync(request);
        //    response.EnsureSuccessStatusCode();

        //    var content = await response.Content.ReadAsStringAsync();
        //    return JsonConvert.DeserializeObject<UserInfo>(content);
        //}
    }
}
