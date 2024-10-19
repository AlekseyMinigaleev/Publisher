using Publisher.Exceptions;
using System.Diagnostics;

namespace Publisher
{
    public class Publisher
    {
        public readonly string SolutionFilePath;
        public readonly string OutputDirectory;
        public readonly string TaskNumber;
        public readonly string BuildDirectory;

        public Publisher(
            string solutionDirectory,
            string outputDirectory,
            string taskNumber)
        {
            SolutionFilePath = GetSolutionFilePath(solutionDirectory);
            OutputDirectory = CheckOutputDirectory(outputDirectory);
            TaskNumber = taskNumber;
            BuildDirectory = Path.Combine(
                OutputDirectory,
                $"build_{TaskNumber}");
        }

        public async Task PublishAsync(CancellationToken cancellationToken)
        {
            var projectFiles = await GetProjectFilesAsync(
                 SolutionFilePath,
                 cancellationToken);

            projectFiles
                .ToList()
                .ForEach(project =>
                {
                    BuildProject(project, PlatformConstants.WINDOWS_X64);
                    BuildProject(project, PlatformConstants.LINUX_X64);
                });
        }

        private static string GetSolutionFilePath(string solitionDirectory) =>
            Directory
                .GetFiles(solitionDirectory, $"*{FileExtensionConstants.SLN}")
                .SingleOrDefault()
                ?? throw new NoSolutionFileException();

        private static string CheckOutputDirectory(string outputDirectory)
        {
            if (Directory.Exists(outputDirectory))
                return outputDirectory;

            Directory.CreateDirectory(outputDirectory);
            Directory.Delete(Path.GetPathRoot(outputDirectory), true);

            return outputDirectory;
        }

        private static async Task<string[]> GetProjectFilesAsync(
            string solutionDirectory,
            CancellationToken cancellationToken)
        {
            var lines = await File.ReadAllLinesAsync(
                 solutionDirectory,
                 cancellationToken);

           var projectFiles = lines
                .Where(x => x.StartsWith(CsprojLinesConstants.PROJECT))
                .Select(GetProjectPath)
                .ToArray();

            return projectFiles;
        }

        private static string GetProjectPath(string projectLine) =>
            projectLine
                .Split(",")
                .Where(part => part.Length > 1)
                .ToArray()[1]
                .Trim()
                .Trim('"');

        private void BuildProject(string projectFile, string runtime)
        {
            var processInfo = CreateProcessInfo(projectFile, runtime);

            try
            {
                ExectuteProcess(processInfo);
            }
            catch (ProcessExitCodeException)
            {
                Console.WriteLine($"'{projectFile}' build was skipped for '{runtime}' platform");
            }
        }

        private ProcessStartInfo CreateProcessInfo(string projectFile, string runtime)
        {
            var arguments = GetBuildProjectProcessArguments(
               projectFile,
               runtime);

            var processInfo = new ProcessStartInfo("dotnet", arguments)
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = Path.GetDirectoryName(SolutionFilePath)
            };

            return processInfo;
        }

        private string GetBuildProjectProcessArguments(
            string projectFile,
            string runtime)
        {
            string configuration = "Release";
            string projectName = Path.GetFileNameWithoutExtension(projectFile);
            string outputPath = Path.Combine(
                BuildDirectory,
                $"{projectName}_{runtime}");

            return $"publish \"{projectFile}\" -c {configuration} -r {runtime} -o \"{outputPath}\"";
        }

        private static void ExectuteProcess(ProcessStartInfo processInfo)
        {
            string processName;
            int processExitCode;

            using (var process = Process.Start(processInfo) ?? throw new Exception())
            {
                process.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);
                process.ErrorDataReceived += (sender, e) => Console.Error.WriteLine(e.Data);

                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                processName = process.ProcessName;

                process.WaitForExit();

                processExitCode = process.ExitCode;
            }

            if (processExitCode != 0)
                throw new ProcessExitCodeException(processName, processExitCode);
        }
    }
}