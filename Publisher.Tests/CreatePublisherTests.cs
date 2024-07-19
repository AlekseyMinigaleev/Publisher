using Publisher.Exceptions;

namespace Publisher.Tests
{
    public class CreatePublisherTests
    {
        [Fact]
        public void Wrong_SolutionDirectory() =>
            Assert.Throws<NoSolutionFileException>(
                () => new Publisher(
                    $"{Directory.GetCurrentDirectory()}",
                    DirectoryPathConstatns.BUILDS));

        [Fact]
        public void Rigth_SolutionDirectory()
        {
            var publisher = new Publisher(
                DirectoryPathConstatns.ALL_PROJECT_SOLUTION,
                DirectoryPathConstatns.BUILDS);

            var slnExtension = Path
                .GetExtension(publisher.SolutionFilePath);

            Assert.Equal($"{FileExtensionConstants.SLN}", slnExtension);
        }

        [Fact]
        public void Exist_Not_Empty_OutputDirectory() =>
            Assert.Throws<NotEmptyOutputDirectoryException>(
                () => new Publisher(
                     DirectoryPathConstatns.ALL_PROJECT_SOLUTION,
                     DirectoryPathConstatns.ALL_PROJECT_SOLUTION));

        [Fact]
        public void Exist_Empty_OutputDirectory()
        {
            Publisher? publisher = null;

            var exception = Record.Exception(() =>
                publisher = new Publisher(
                    DirectoryPathConstatns.ALL_PROJECT_SOLUTION,
                    DirectoryPathConstatns.EXIST_EMPTY_OUTPUT));

            Assert.Null(exception);
            Assert.NotNull(publisher);
        }

        [Fact]
        public void Not_Exist_OutputDirectory()
        {
            Publisher? publisher = null;

            var exception = Record.Exception(() =>
                publisher = new Publisher(
                    DirectoryPathConstatns.ALL_PROJECT_SOLUTION,
                    DirectoryPathConstatns.NOT_EXIST_OUTPUT));

            Assert.Null(exception);
            Assert.NotNull(publisher);
        }
    }
}