using System;
using System.IO;
using System.Threading.Tasks;

namespace HospitalApp.Tests
{
    internal class Program
    {
        static async Task<int> Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("🚀 KHỞI ĐỘNG SINH TEST...");
            Console.ResetColor();

            try
            {
                // 🧭 Xác định đường dẫn thư mục hiện tại
                string baseDir = AppContext.BaseDirectory;
                string projectDir = Directory.GetParent(baseDir)!.Parent!.Parent!.Parent!.FullName; // fix: đi lên 4 cấp
                string solutionDir = Directory.GetParent(projectDir)!.FullName;

                // 📂 Thư mục chứa code Service (nơi cần sinh test)
                string serviceDir = Path.Combine(solutionDir, "Service");

                // 📂 Thư mục chứa project test đích (HospitalApp.UnitTests)
                string unitTestDir = Path.Combine(solutionDir, "HospitalApp.UnitTests");
                Directory.CreateDirectory(unitTestDir);

                Console.WriteLine($"📁 Service folder : {serviceDir}");
                Console.WriteLine($"📂 Output folder  : {unitTestDir}");
                Console.WriteLine($"🔍 BaseDir      : {baseDir}");
                Console.WriteLine($"🔍 ProjectDir   : {projectDir}");
                Console.WriteLine($"🔍 SolutionDir  : {solutionDir}");

                // ❌ Nếu không tìm thấy thư mục Service
                if (!Directory.Exists(serviceDir))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ Không tìm thấy thư mục Service!");
                    Console.ResetColor();
                    Pause();
                    return 1;
                }

                // 🔎 Đếm file trong thư mục Service
                var count = Directory.GetFiles(serviceDir, "*.cs", SearchOption.TopDirectoryOnly).Length;
                Console.WriteLine($"🔎 Tìm thấy {count} file .cs trong Service.");

                if (count == 0)
                {
                    Console.WriteLine("⚠️ Không có file nào để sinh test.");
                    Pause();
                    return 0;
                }

                // 🚀 Gọi generator sinh test
                await GeminiBulkTestGenerator.GenerateTestsForFolderAsync(serviceDir, unitTestDir);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n🎉 HOÀN TẤT!");
                Console.ResetColor();
                Console.WriteLine($"📍 File test đã sinh vào: {unitTestDir}");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("💥 LỖI:");
                Console.WriteLine(ex);
                Console.ResetColor();
            }

            Pause();
            return 0;
        }

        private static void Pause()
        {
            Console.WriteLine("\nNhấn phím bất kỳ để thoát . . .");
            try { Console.ReadKey(true); } catch { }
        }
    }
}
