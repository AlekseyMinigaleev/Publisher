using Publisher.Exceptions;
using System.Diagnostics;

namespace Publisher
{
    public class Publisher
    {
        public readonly string SolutionFilePath;
        public readonly string OutputDirectory;

        public Publisher(
            string solutionDirectory,
            string outputDirectory)
        {
            SolutionFilePath = GetSolutionFilePath(solutionDirectory);
            OutputDirectory = CheckOutputDirectory(outputDirectory);
        }

        public async Task PublishAsync(CancellationToken cancellationToken)
        {
            var projFiles = await GetProjectFilesAsync(
                SolutionFilePath,
                cancellationToken);

            foreach (var projectFile in projFiles)
            {
                BuildProject(projectFile, PlatformConstants.WINDOWS_X64);
                BuildProject(projectFile, PlatformConstants.LINUX_X64);
            }
        }

        private static string GetSolutionFilePath(string solitionDirectory) =>
            Directory
                .GetFiles(solitionDirectory, $"*{FileExtensionConstants.SLN}")
                .SingleOrDefault()
                ?? throw new NoSolutionFileException();

        private static string CheckOutputDirectory(string outputDirectory)
        {
            if (!Directory.Exists(outputDirectory))
                return outputDirectory;

            var isEmptyDirectory = !Directory
                .GetFiles(outputDirectory)
                .Any();

            if (isEmptyDirectory)
                return outputDirectory;

            throw new NotEmptyOutputDirectoryException();
        }

        private static async Task<string[]> GetProjectFilesAsync(
            string solutionDirectory,
            CancellationToken cancellationToken) =>
            (await File
                .ReadAllLinesAsync(solutionDirectory, cancellationToken))
                .Where(x => x.StartsWith("Project("))
                .Select(x =>
                {
                    var path = x
                        .Split(",")
                        .Where(part => part.Length > 1)
                        .ToArray()
                        [1].Trim()
                        .Trim('"');

                    return path;
                })
                .ToArray();

        private void BuildProject(string projectFile, string runtime)
        {
            string configuration = "Release";
            string projectName = Path.GetFileNameWithoutExtension(projectFile);
            string outputPath = Path.Combine(
                OutputDirectory,
                $"{projectName}_{runtime}");

            var arguments = 
                $"publish \"{projectFile}\" -c {configuration} -r {runtime} -o \"{outputPath}\"";

            var processInfo = new ProcessStartInfo("dotnet", arguments)
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = Path.GetDirectoryName(SolutionFilePath)
            };

            using var process = 
                Process.Start(processInfo)
                ?? throw new Exception();

            process.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);
            process.ErrorDataReceived += (sender, e) => Console.Error.WriteLine(e.Data);

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            process.WaitForExit();
            if (process.ExitCode != 0)
                Console.WriteLine($"Error building project {projectName} for {runtime}");
        }
    }
}