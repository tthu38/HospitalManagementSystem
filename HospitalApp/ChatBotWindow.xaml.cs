using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using Service;

namespace HospitalApp
{
    public partial class ChatBotWindow : Window
    {
        private readonly HttpClient _http;
        private readonly DoctorService _doctorService = new();
        private readonly RoomService _roomService = new();

        //  private const string GeminiApiKey = "AIzaSyDYK1kBmcDE7vh99Qz2e_q83XPigK3SaIY";
        private const string GeminiApiKey = "AIzaSyAos6ExaIcyvj2PIcdztQBsfcleNAntYBU";
        private const string GeminiModel = "gemini-2.0-flash"; // ✅ dùng model ổn định (có sẵn trong v1beta)

        public ChatBotWindow()
        {
            InitializeComponent();

            _http = new HttpClient
            {
                BaseAddress = new Uri("https://generativelanguage.googleapis.com/")
            };
        }

        private async void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            var userInput = txtInput.Text.Trim();
            if (string.IsNullOrEmpty(userInput)) return;

            AppendLine($"👤 {userInput}");
            txtInput.Clear();

            try
            {
                // 🏥 Ghép dữ liệu bệnh viện thật (doctors + available rooms)
                var doctors = _doctorService.GetAll()
                    .Select(d => $"{d.FullName} ({d.Specialization})")
                    .ToList();

                var availableRooms = _roomService.GetAvailable()
                    .Select(r => $"{r.RoomNumber} - {r.RoomType}")
                    .ToList();

                string hospitalData =
                    $"Danh sách bác sĩ: {string.Join(", ", doctors)}. " +
                    $"Phòng trống: {string.Join(", ", availableRooms)}.";

                // 🧠 Tạo prompt kết hợp dữ liệu thật
                var payload = new
                {
                    contents = new[]
                    {
                        new {
                            parts = new[]
                            {
                                new {
                                    text = $"Bạn là trợ lý ảo của bệnh viện. Dưới đây là dữ liệu thật:\n{hospitalData}\n\n" +
                                           $"Dựa trên dữ liệu này, hãy trả lời câu hỏi sau của người dùng một cách ngắn gọn và thân thiện:\n{userInput}"
                                }
                            }
                        }
                    }
                };

                var json = JsonSerializer.Serialize(payload);

                var res = await _http.PostAsync(
                    $"v1beta/models/{GeminiModel}:generateContent?key={GeminiApiKey}",
                    new StringContent(json, Encoding.UTF8, "application/json")
                );

                var body = await res.Content.ReadAsStringAsync();

                if (!res.IsSuccessStatusCode)
                {
                    AppendLine($"⚠️ Lỗi API: {res.StatusCode}\n{body}");
                    return;
                }

                using var doc = JsonDocument.Parse(body);
                var reply = doc.RootElement
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString();

                AppendLine($"🤖 {reply}");
            }
            catch (Exception ex)
            {
                AppendLine($"⚠️ Exception: {ex.Message}");
            }
        }

        private void AppendLine(string text)
        {
            txtConversation.Text += text + Environment.NewLine + Environment.NewLine;
        }
    }
}
