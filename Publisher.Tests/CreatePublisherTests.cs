using Publisher.Exceptions;

namespace Publisher.Tests
{
    public class CreatePublisherTests
    {
        [Fact]
        public void Wrong_SolutionDirectory()
        {
            var localSolutionDirectory = Utils.GetLocalSolutionDirectory();

            Assert.Throws<NoSolutionFileException>(
                () => new Publisher(
                    $"{Directory.GetCurrentDirectory()}",
                    ""));
        }

        [Fact]
        public void Rigth_SolutionDirectory()
        {
            var localSolutionDirectory = Utils.GetLocalSolutionDirectory();

            var publisher = new Publisher(
                localSolutionDirectory,
                $"{localSolutionDirectory}/new folder");

            var slnExtension = Path
                .GetExtension(publisher.SolutionFilePath);

            Assert.Equal(".sln", slnExtension);
        }

        [Fact]
        public void Exist_Not_Empty_OutputDirectory()
        {
            var localSolutionDirectory = Utils.GetLocalSolutionDirectory();

            Assert.Throws<NotEmptyOutputDirectoryException>(
                () => new Publisher(
                    localSolutionDirectory,
                    localSolutionDirectory));
        }

        [Fact]
        public void Exist_Empty_OutputDirectory()
        {
            var localSolutionDirectory = Utils.GetLocalSolutionDirectory();

            Publisher? publisher = null;

            var exception = Record.Exception(() =>
                publisher = new Publisher(
                    localSolutionDirectory,
                    $"{localSolutionDirectory}\\Output"));

            Assert.Null(exception);
            Assert.NotNull(publisher);
        }

        [Fact]
        public void Not_Exist_OutputDirectory()
        {
            var localSolutionDirectory = Utils.GetLocalSolutionDirectory();

            Publisher? publisher = null;

            var exception = Record.Exception(() =>
                publisher = new Publisher(
                    localSolutionDirectory,
                    $"{localSolutionDirectory}/new folder"));

            Assert.Null(exception);
            Assert.NotNull(publisher);
        }

    }
}