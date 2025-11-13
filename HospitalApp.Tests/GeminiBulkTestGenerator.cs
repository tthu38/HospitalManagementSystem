using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HospitalApp.Tests
{
    public static class GeminiBulkTestGenerator
    {
        private const string ApiKey = "AIzaSyAos6ExaIcyvj2PIcdztQBsfcleNAntYBU";
        private const string Model = "gemini-2.0-flash";

        public static async Task GenerateTestsForFolderAsync(string sourceFolder, string outputFolder)
        {
            if (!Directory.Exists(sourceFolder))
                throw new DirectoryNotFoundException($"Không tìm thấy thư mục: {sourceFolder}");

            Directory.CreateDirectory(outputFolder);
            var files = Directory.GetFiles(sourceFolder, "*.cs", SearchOption.TopDirectoryOnly);

            Console.WriteLine($"🔍 Đã tìm thấy {files.Length} file .cs trong {sourceFolder}");
            if (files.Length == 0) return;

            foreach (var file in files)
            {
                Console.WriteLine($"\n📄 Sinh test cho: {Path.GetFileName(file)}");
                try
                {
                    await GenerateTestForFileAsync(file, outputFolder);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"❌ Lỗi khi sinh test cho {file}: {ex.Message}");
                    Console.ResetColor();
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✅ Hoàn tất sinh test cho tất cả file!");
            Console.ResetColor();
        }

        private static async Task GenerateTestForFileAsync(string sourcePath, string outputDir)
        {
            string classCode = await File.ReadAllTextAsync(sourcePath);

            // 🧠 PROMPT SIÊU SẠCH
            string prompt = $$"""
Viết Unit Test C# dùng xUnit và Moq cho class sau.

Yêu cầu:
- Target .NET 8
- Chỉ viết mã C# hợp lệ (KHÔNG markdown, KHÔNG mô tả tiếng Anh)
- Không được sinh thêm phương thức không có trong class
- Không tạo property như Id, Name... nếu không thấy trong mã
- Khi cần đối tượng, chỉ dùng new Type()
- Dùng Moq để mô phỏng repository hoặc dependency
- Arrange / Act / Assert rõ ràng
- Không truy cập DB, file, hay static global state
- Namespace của test: HospitalApp.UnitTests
- Bắt buộc import:
  using Xunit;
  using Moq;
  using Repository;
  using Service;
  using Business;

{{classCode}}
""";

            string endpoint = $"https://generativelanguage.googleapis.com/v1beta/models/{Model}:generateContent?key={ApiKey}";

            using var http = new HttpClient();
            var payload = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[] { new { text = prompt } }
                    }
                },
                generationConfig = new { temperature = 0.2 }
            };

            string json = JsonSerializer.Serialize(payload);
            HttpResponseMessage res = await http.PostAsync(
                endpoint,
                new StringContent(json, Encoding.UTF8, "application/json")
            );

            string body = await res.Content.ReadAsStringAsync();
            if (!res.IsSuccessStatusCode)
            {
                Console.WriteLine($"⚠️ API lỗi: {body}");
                return;
            }

            string? text = null;
            try
            {
                using var doc = JsonDocument.Parse(body);
                text = doc.RootElement
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString();
            }
            catch
            {
                Console.WriteLine("⚠️ Không parse được JSON response.");
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine("⚠️ Gemini không trả về code.");
                return;
            }

            // 🧹 Làm sạch code
            string cleanCode = CleanCodeOutput(text);

            // 🧾 Ép using chuẩn
            string fullCode =
                "using Xunit;\nusing Moq;\nusing Repository;\nusing Service;\nusing Business;\n\n" + cleanCode;

            string fileName = Path.GetFileNameWithoutExtension(sourcePath) + "Tests.cs";
            string outputPath = Path.Combine(outputDir, fileName);

            await File.WriteAllTextAsync(outputPath, fullCode, Encoding.UTF8);
            Console.WriteLine($"✅ Đã ghi file test: {outputPath}");
        }

        private static string CleanCodeOutput(string raw)
        {
            // Loại bỏ markdown và mô tả văn bản
            string cleaned = raw;

            // Xóa dấu ``` và các dòng giải thích
            cleaned = Regex.Replace(cleaned, "```[a-zA-Z]*", "", RegexOptions.Multiline);
            cleaned = cleaned.Replace("```", "");
            cleaned = Regex.Replace(cleaned, @"(?m)^\s*//.*$", ""); // xóa comment
            cleaned = Regex.Replace(cleaned, @"(?m)^\s*(This|The|A|An|Use|Ensure|Make|Mock|Verify|Add|Return|Create|Act|Assert|Arrange)\b.*$", ""); // bỏ giải thích tiếng Anh

            // Xóa ký tự không hợp lệ
            cleaned = cleaned.Replace("`", "")
                             .Replace("´", "")
                             .Replace("–", "-")
                             .Replace("…", "...")
                             .Trim();

            // Giữ lại namespace, class, methods
            return cleaned;
        }
    }
}
