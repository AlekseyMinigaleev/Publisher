namespace Publisher.Tests
{
    public class PublishTests
    {
        [Fact]
        public async Task Publish_AllProjects()
        {
            var publisher = CreatePublisher();

            await publisher.PublishAsync(CancellationToken.None);

            var existProjectDirs = Directory
                .GetDirectories(DirectoryPathConstatns.ALL_PROJECT_SOLUTION)
                .Where(x => Directory
                    .GetFiles(
                        x,
                        $"*{FileExtensionConstants.CJPROJ}")
                    .Any());

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

            Assert.Equal(winExistProjectDirsCount, winPublishDirsCount);
            Assert.Equal(linuxExistProjectDirsCount, linuxPublisDirsCount);
        }

        [Fact]
        public async Task Create_Build_Folder()
        {
            var publisher = CreatePublisher();

            await publisher.PublishAsync(CancellationToken.None);

            var isFolderCreated = Directory
                .Exists(publisher.BuildDirectory);

            Assert.True( isFolderCreated);
        }

        private static Publisher CreatePublisher() =>
            new (
                DirectoryPathConstatns.ALL_PROJECT_SOLUTION,
                DirectoryPathConstatns.BUILDS,
                new Random().Next());
    }
}