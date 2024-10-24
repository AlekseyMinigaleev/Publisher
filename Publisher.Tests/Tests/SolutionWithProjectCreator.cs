using System.Diagnostics;

namespace Publisher.Tests.Tests
{
    internal class SolutionWithProjectCreator
    {
        public string[] ProjectTypes = new[]
        {
            "console",
            "webapp",
            "mvc",
            "blazorserver",
            "blazorwasm",
            "classlib",
            "wpf",
            "winforms",
            "worker",
            "webapi",
            "xunit"
        };

        public int MyProperty { get; set; }

        public static async Task CreateAllProjectsAsync()
        {
            var solutionName = "Test";
            var directory = new DirectoryInfo(DirectoryPathsForPublisherCreationTests.OutputDirectoryForTestBuilds);

            var a = directory.Parent?.FullName
                ?? throw new InvalidOperationException();

            var solutionPath = Path.Combine(a, solutionName);

            if (!Directory.Exists(solutionPath))
                Directory.CreateDirectory(solutionPath);

            await RunDotnetCommandAsync($"new sln -n {solutionName} -o {solutionPath} --force");

            foreach (var projectType in ProjectTypes)
            {
                var projectName = $"MyProject_{projectType}";
                var projectPath = Path.Combine(solutionPath, projectName);

                var solutionFullName = $"{Path.Combine(solutionPath, solutionName)}{FileExtensionConstants.SLN}";
                var projectFullName = $"{Path.Combine(projectPath, projectName)}{FileExtensionConstants.CJPROJ}";

                await RunDotnetCommandAsync($"new {projectType} -n {projectName} -o {projectPath} --force");

                await RunDotnetCommandAsync($"sln {solutionFullName} add {projectFullName}");
            }
        }

        private static async Task RunDotnetCommandAsync(string arguments)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = new Process { StartInfo = startInfo };
            process.Start();

            string output = await process.StandardOutput.ReadToEndAsync();
            string error = await process.StandardError.ReadToEndAsync();

            process.WaitForExit();

            if (process.ExitCode != 0)
                throw new Exception($"Command failed: {error}");
        }
    }
}