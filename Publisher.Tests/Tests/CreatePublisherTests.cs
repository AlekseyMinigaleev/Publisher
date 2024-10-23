using Publisher.Exceptions;

namespace Publisher.Tests.Tests
{
    public class CreatePublisherTests
    {
        [Fact]
        public void Wrong_SolutionDirectory()
        {
            Assert.Throws<NoSolutionFileException>(
                () => new Publisher(
                    $"{Directory.GetCurrentDirectory()}",
                    DirectoryPathsForPublisherCreationTests.OutputDirectoryForTestBuilds,
                    "1"));
        }

        [Fact]
        public void Correctly_Set_SoulutionFilePath_With_Right_SolutionDirectory()
        {
            var publisher = new Publisher(
                DirectoryPathsForPublisherCreationTests.CurrentProjectDirectoryPath,
                DirectoryPathsForPublisherCreationTests.OutputDirectoryForTestBuilds,
                "1");

            var slnExtension = Path
                .GetExtension(publisher.SolutionFilePath);

            Assert.Equal($"{FileExtensionConstants.SLN}", slnExtension);
        }

        [Fact]
        public void Exist_OutputDirectory()
        {
            if (!Directory.Exists(DirectoryPathsForPublisherCreationTests.OutputDirectoryForTestBuilds))
                Directory.CreateDirectory(DirectoryPathsForPublisherCreationTests.OutputDirectoryForTestBuilds);

            Publisher? publisher = null;

            var exception = Record.Exception(() =>
                publisher = new Publisher(
                    DirectoryPathsForPublisherCreationTests.CurrentProjectDirectoryPath,
                    DirectoryPathsForPublisherCreationTests.OutputDirectoryForTestBuilds,
                    "1"));

            Assert.Null(exception);
            Assert.NotNull(publisher);
        }

        [Fact]
        public void Not_Exist_OutputDirectory()
        {
            Publisher? publisher = null;

            var newDirectoryPath = $"{DirectoryPathsForPublisherCreationTests.OutputDirectoryForTestBuilds}\\foo\\bar\\bazz";
            var exception = Record.Exception(() =>
                publisher = new Publisher(
                    DirectoryPathsForPublisherCreationTests.CurrentProjectDirectoryPath,
                    newDirectoryPath,
                    "1"));

            Assert.False(Directory.Exists(newDirectoryPath));
            Assert.Null(exception);
            Assert.NotNull(publisher);
        }
    }
}