using System;
using System.Diagnostics;
using System.IO;

namespace C_Console4._8 {
    internal class Program {
        static void Main(string[] args) {
            javaTest();
        }

        static void javaTest() {
            // exe 기준 상대경로
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            string javaPath = Path.Combine(baseDir, "binaryExtractor", "runtime", "bin", "java.exe");
            string jarPath = Path.Combine(baseDir, "binaryExtractor", "PptExtractor.jar");
            string[] jars = Directory.GetFiles(Path.Combine(baseDir, "binaryExtractor", "lib"), "*.jar");
            string libClassPath = string.Join(";", jars);
            string classPath = $"\"{jarPath};{libClassPath}\"";

            if (!File.Exists(javaPath)) {
                Console.WriteLine("내부 JRE java.exe를 찾을 수 없습니다!");
                return;
            }

            if (!File.Exists(jarPath)) {
                Console.WriteLine("JAR 파일을 찾을 수 없습니다!");
                return;
            }

            // 전달할 PowerPoint 파일 경로
            string pptFile = @"C:\Users\dongjun.lee\Documents\dataloadertest\pptTest.ppt";
            string outputJson = @"C:\Users\dongjun.lee\Documents\dataloadertest\result.json";

            var startInfo = new ProcessStartInfo {
                FileName = javaPath,
                Arguments = $"-cp {classPath} PptExtractor \"{pptFile}\" \"{outputJson}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (var process = Process.Start(startInfo)) {
                string stdout = process.StandardOutput.ReadToEnd();
                string stderr = process.StandardError.ReadToEnd();
                process.WaitForExit();

                Console.WriteLine("=== STDOUT ===");
                Console.WriteLine(stdout);

                Console.WriteLine("=== STDERR ===");
                Console.WriteLine(stderr);
            }

        }
    }


}
