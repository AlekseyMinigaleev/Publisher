namespace Publisher.Tests
{
    public class PublishTests
    {
        [Fact]
        public async Task Create_File_SystemAsync()
        {
            var localSolutionDirectory = Utils
                .GetLocalSolutionDirectory();
            var publisher = new Publisher(
                localSolutionDirectory,
                $"{localSolutionDirectory}\\Builds");

            await publisher.PublishAsync(CancellationToken.None);

            var existProjectDirs = Directory
                .GetDirectories(localSolutionDirectory)
                .Where(x => Directory.GetFiles(x, "*.csproj").Any());

            var winPublishDirsCount = Directory
                .GetDirectories(
                    publisher.OutputDirectory,
                    $"*_{PlatformConstants.WINDOWS_X64}")
                .Length;

            var linuxPublisDirsCount = Directory
                .GetDirectories(
                    publisher.OutputDirectory,
                    $"*_{PlatformConstants.LINUX_X64}")
                .Length;

            var winExistProjectDirsCount = existProjectDirs.Count();
            var linuxExistProjectDirsCount = existProjectDirs.Count();

            var isWpfProjectDirExist = existProjectDirs
                .Where(dir =>
                {
                    var csprojFile = Directory
                        .GetFiles(dir, "*.csproj")
                        .Single();

                    var wpfline = File.ReadAllLines(csprojFile)
                        .Select(x => x.Trim().Trim('"'))
                        .Where(x => x.Contains("<UseWPF>"))
                        .Any();

                    return wpfline;
                })
                .Any();

            if (isWpfProjectDirExist)
                linuxExistProjectDirsCount--;

            Assert.Equal(winExistProjectDirsCount, winPublishDirsCount);
            Assert.Equal(linuxExistProjectDirsCount, linuxPublisDirsCount);
        }
    }
}