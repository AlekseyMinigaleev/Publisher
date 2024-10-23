using System.Diagnostics;

namespace Publisher.Tests.Tests
{
    public class PublishTests
    {
        [Fact]
        public async Task Publish_AllProjects()
        {
            await CreateAllProjectsAsync();
            //var publisher = CreatePublisher();

            //await publisher.PublishAsync(CancellationToken.None);

            //var existProjectDirs = Directory
            //    .GetDirectories(DirectoryPathConstatns.ALL_PROJECT_SOLUTION)
            //    .Where(x => Directory
            //        .GetFiles(
            //            x,
            //            $"*{FileExtensionConstants.CJPROJ}")
            //        .Any());

            //var winPublishDirsCount = Directory
            //    .GetDirectories(
            //        publisher.BuildDirectory,
            //        $"*_{PlatformConstants.WINDOWS_X64}")
            //    .Length;

            //var linuxPublisDirsCount = Directory
            //    .GetDirectories(
            //        publisher.BuildDirectory,
            //        $"*_{PlatformConstants.LINUX_X64}")
            //    .Length;

            //var winExistProjectDirsCount = existProjectDirs.Count();
            //var linuxExistProjectDirsCount = existProjectDirs.Count();

            //Assert.Equal(winExistProjectDirsCount, winPublishDirsCount);
            //Assert.Equal(linuxExistProjectDirsCount, linuxPublisDirsCount);
        }

        [Fact]
        public async Task Create_Build_Folder()
        {
            var publisher = new Publisher(
                DirectoryPathsForPublisherCreationTests.CurrentProjectDirectoryPath,
                DirectoryPathsForPublisherCreationTests.OutputDirectoryForTestBuilds,
                "test");

            await publisher.PublishAsync(CancellationToken.None);

            Assert.True(Directory.Exists(publisher.BuildDirectory));
        }

        [Fact]
        public async Task PublishAsync_Should_Create_Two_Distinct_Builds_When_Called_Twice_With_Same_Output_Directory()
        {
            var publisher = new Publisher(
                DirectoryPathsForPublisherCreationTests.CurrentProjectDirectoryPath,
                DirectoryPathsForPublisherCreationTests.OutputDirectoryForTestBuilds,
                "test");

            await publisher.PublishAsync(CancellationToken.None);
            var firstBuildDirectory = publisher.BuildDirectory;

            await publisher.PublishAsync(CancellationToken.None);
            var secondBuildDirectory = publisher.BuildDirectory;

            Assert.NotEqual(firstBuildDirectory, secondBuildDirectory);
            Assert.Equal("test", firstBuildDirectory);
            Assert.Equal("test(1)", secondBuildDirectory);
        }

        private static async Task CreateAllProjectsAsync()
        {
            var solutionName = "Test";
            var directory = new DirectoryInfo(DirectoryPathsForPublisherCreationTests.OutputDirectoryForTestBuilds);

            var a = directory.Parent?.FullName
                ?? throw new InvalidOperationException();

            var solutionPath = Path.Combine(a, solutionName);

            if (!Directory.Exists(solutionPath))
                Directory.CreateDirectory(solutionPath);

            await RunDotnetCommandAsync($"new sln -n {solutionName} -o {solutionPath} --force");

            var projectTypes = new[]
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

            foreach (var projectType in projectTypes)
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