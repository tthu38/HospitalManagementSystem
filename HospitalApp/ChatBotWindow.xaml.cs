using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Business;
using Service;

namespace HospitalApp
{
    public partial class ChatBotWindow : Window
    {
        private readonly HttpClient _http;
        private readonly DoctorService _doctorService = new();
        private readonly RoomService _roomService = new();

        private const string GeminiApiKey = "AIzaSyAos6ExaIcyvj2PIcdztQBsfcleNAntYBU";
        private const string GeminiModel = "gemini-2.0-flash";

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

            AddMessageBubble(userInput, true);
            txtInput.Clear();

            try
            {
                // Collect hospital real data
                var doctors = _doctorService.GetAll()
                    .Select(d => $"{d.FullName} ({d.Specialization})")
                    .ToList();

                var rooms = _roomService.GetAvailable()
                    .Select(r => $"{r.RoomNumber} - {r.RoomType}")
                    .ToList();

                string hospitalData =
                    $"Bác sĩ hiện có: {string.Join(", ", doctors)}.\n" +
                    $"Phòng còn trống: {string.Join(", ", rooms)}.";

                // Build request
                var payload = new
                {
                    contents = new[]
                    {
                        new {
                            parts = new[]
                            {
                                new {
                                    text = $"Bạn là trợ lý ảo của bệnh viện.\nDữ liệu thật: {hospitalData}\n\n" +
                                           $"Câu hỏi: {userInput}"
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
                    AddMessageBubble($"⚠️ API Error: {res.StatusCode}", false);
                    return;
                }

                using var docJson = JsonDocument.Parse(body);
                var reply = docJson.RootElement
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString();

                AddMessageBubble(reply, false);
            }
            catch (Exception ex)
            {
                AddMessageBubble($"⚠️ {ex.Message}", false);
            }
        }

        private void AddMessageBubble(string text, bool isUser)
        {
            var bubble = new Border
            {
                Style = (Style)FindResource(isUser ? "UserBubbleStyle" : "BotBubbleStyle")
            };

            var tb = new TextBlock
            {
                Text = text,
                Style = (Style)FindResource("BubbleTextStyle"),
                Foreground = isUser ? Brushes.White : Brushes.Black
            };

            bubble.Child = tb;

            chatPanel.Children.Add(bubble);
            scroll.ScrollToBottom();
        }
    }
}
