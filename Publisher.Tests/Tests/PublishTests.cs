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
    }
}